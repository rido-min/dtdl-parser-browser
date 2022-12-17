// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

import { dotnet } from './dotnet.js'

const { setModuleImports, getAssemblyExports, getConfig } = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

const config = getConfig();
const exports = await getAssemblyExports(config.mainAssemblyName);

await dotnet.run();

const out = document.getElementById('out')
const el = document.getElementById('dtdl-text')

const validate = async () => out.innerText = await exports.MyClass.ParseDTDL(el.value)

el.onkeyup = validate
(async () => {
    console.log('window load')
    await validate()
})()
