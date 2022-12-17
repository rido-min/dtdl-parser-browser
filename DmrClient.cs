using DTDLParser;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace wb;

internal class DmrClient
{
    static Dictionary<string,string> modelDefinitions = new();

    static Task<string> ResolveDtmiAsync(string dtmi, string dmrBasePath = "https://devicemodels.azure.com") =>
       new HttpClient().GetStringAsync($"{dmrBasePath}/{dtmi.Replace(':', '/').Replace(';', '-').ToLowerInvariant()}.json");

    internal static IEnumerable<string> DtmiResolver(IReadOnlyCollection<Dtmi> dtmis) => DtmiResolverAsync(dtmis).Result;

    internal static async Task<IEnumerable<string>> DtmiResolverAsync(IReadOnlyCollection<Dtmi> dtmis)
    {
        IEnumerable<string> dtmiStrings = dtmis.Select(s => s.AbsoluteUri);
        List<string> models = new();
        foreach (var dtmi in dtmiStrings)
        {
            if (!modelDefinitions.ContainsKey(dtmi))
            {
                var content = await ResolveDtmiAsync(dtmi);
                modelDefinitions.Add(dtmi,content);
            }
            models.Add(modelDefinitions[dtmi]);
        }
        return models;
    }
}
