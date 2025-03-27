const fs = require('node:fs')

module.exports = ({github, context}) => {
    const results = loadBenchmarkResults();
    const commentContent = formatResultsAsGithubTable(results);
    github.rest.issues.createComment({
        issue_number: context.issue.number,
        owner: context.repo.owner,
        repo: context.repo.repo,
        body: commentContent
    });
}

function loadBenchmarkResults() {
    const filePath = 'Benchmarks/AnimalTrack.Repository.Benchmarks/BenchmarkDotNet.Artifacts/results/AnimalTrack.Repository.Benchmarks.ReadAnimalsBenchmark-report.json';
    const fileContent = fs.readFileSync(filePath);
    const parsedFileContent = JSON.parse(fileContent);
    const benchmarks = parsedFileContent['Benchmarks']
    
    const results = [];
    for (const benchmark of benchmarks) {
        const stats = benchmark['Statistics'];
        const memory = benchmark['Memory'];
        const result = {
            name: benchmark['Name'],
            mean: stats['Mean'],
            gen0: memory['Gen0'],
            gen1: memory['Gen1'],
            gen2: memory['Gen2'],
            allocated: memory['BytesAllocatedPerOperation'],
        }
        results.push(result);
    }
    
    return results;
}

function formatResultsAsGithubTable(results) {
    let commentContent = '|Benchmark|Mean|Gen0|Gen1|Gen2|Allocations|\n';
    commentContent += '|---|---|---|---|---|---|\n';
    
    for (const result of results) {
        commentContent += `| ${result.name} | ${result.mean} | ${result.gen0} | ${result.gen1} | ${result.gen2} | ${result.allocated} |\n`;
    }
    
    return commentContent;
}