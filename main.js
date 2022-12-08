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

let lastJsonErr = ''
const isValidJson = s => {
    let res = false
    try {
        JSON.parse(s)
        res = true
    } catch (err) {
        console.log(err)
        lastJsonErr = err.message
    }
    return res
}

const out = document.getElementById('out')
const el = document.getElementById('dtdl-text')
el.onkeyup = async () => {
    if (isValidJson(el.value)) {
        //out.innerText = el.value.length
        const res = await exports.MyClass.ParseDTDL(el.value)
        out.innerText = res
    } else {
        out.innerText = lastJsonErr
    }
}
