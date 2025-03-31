const fs = require("node:fs");

module.exports = ({github, context}) => {
    const masterResults = loadBenchmarkResults('./master-benchmark-results.json');
    const branchResults = loadBenchmarkResults('./branch-benchmark-results.json');
    
    let commentContent = '| Benchmark | Mean | Allocations |\n'
    commentContent += '|---|---|---|\n'
    
    for(const branchResult of branchResults) {
        const masterResult = masterResults.filter(res => res.name == branchResult.name)[0];
        
        const timingsDiff = round(percentageDifference(branchResult.mean, masterResult.mean), 2);
        const allocationsDiff = round(percentageDifference(branchResult.allocated, masterResult.allocated), 2);
        commentContent += `| ${branchResult.name} | ${formatTime(branchResult.mean)} (${timingsDiff < 0 ? '' : '+'}${timingsDiff}%) | ${formatBytes(branchResult.allocated)} (${allocationsDiff < 0 ? '' : '+'}${allocationsDiff}%) |\n`
    }
    
    github.rest.issues.createComment({
        issue_number: context.issue.number,
        owner: context.repo.owner,
        repo: context.repo.repo,
        body: commentContent
    });
}

function loadBenchmarkResults(filePath) {
    const fileContent = fs.readFileSync(filePath);
    const parsedFileContent = JSON.parse(fileContent);
    const benchmarks = parsedFileContent['Benchmarks']

    const results = [];
    for (const benchmark of benchmarks) {
        const stats = benchmark['Statistics'];
        const memory = benchmark['Memory'];
        const result = {
            name: benchmark['Type'],
            mean: parseFloat(stats['Mean']),
            gen0: memory['Gen0Collections'],
            gen1: memory['Gen1Collections'],
            gen2: memory['Gen2Collections'],
            allocated: parseFloat(memory['BytesAllocatedPerOperation']),
        }
        results.push(result);
    }

    return results;
}

function formatBytes(bytes) {
    const decimals = 2;

    const k = 1024
    const dm = decimals < 0 ? 0 : decimals
    const sizes = ['Bytes', 'Kb', 'Mb', 'Gb']

    const i = Math.floor(Math.log(bytes) / Math.log(k))

    return `${parseFloat((bytes / Math.pow(k, i)).toFixed(dm))}${sizes[i]}`
}

function formatTime(nanoseconds) {
    const decimals = 2;

    const k = 1000;
    const dm = decimals < 0 ? 0 : decimals;
    const sizes = ['ns', 'us', 'ms', 's'];

    const i = Math.floor(Math.log(nanoseconds) / Math.log(k))

    return `${parseFloat((nanoseconds / Math.pow(k, i)).toFixed(dm))} ${sizes[i]}`
}

function percentageDifference(newValue, oldValue) {
    return ((newValue - oldValue) / oldValue) * 100;
}

function round(value, decimals) {
    var k = 10 * decimals;
    return Math.round(value * k) / k;
}
