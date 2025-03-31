const fs = require("node:fs");

module.exports = ({github, context}) => {
    const masterResults = loadBenchmarkResults('./master-benchmark-results.json');
    const branchResults = loadBenchmarkResults('./branch-benchmark-results.json');
    
    let commentContent = '| Benchmark | Timings | Allocations |\n'
    commentContent += '|---|---|---|\n'
    
    for(const branchResult of branchResults) {
        const masterResult = masterResults.filter(res => res.name == branchResult.name)[0];
        
        const name = branchResult.name;
        const timingsDiff = percentageDifference(branchResult.mean, masterResult.mean);
        const allocationsDiff = percentageDifference(branchResult.allocated, masterResult.allocated);
        
        commentContent += `| ${name} | ${round(timingsDiff, 2)}% | ${round(allocationsDiff, 2)} |\n`
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

function percentageDifference(newValue, oldValue) {
    return ((newValue - oldValue) / oldValue) * 100;
}

function round(value, decimals) {
    var k = 10 * decimals;
    return Math.round(value * k) / k;
}
