import { readFileSync, readdirSync } from 'node:fs';
const solutionsFolder = '2024/solutions';
const dataFolder = '2024/data';

async function runSolutions() {
    const totalStart = process.hrtime();
    const files = readdirSync(solutionsFolder);

    const results = [];

    await Promise.all(files.map(async (file) => {
        const solution = await import(`./${solutionsFolder}/${file}`);
        let content = readFileSync(`${dataFolder}/${solution.testFile}`, 'utf-8');
        let result = solution.solve(content);

        if (result !== solution.testOutputExpected) {
            results.push({file, passed: false, result, expected: solution.testOutputExpected});
        }
        else {
            content = readFileSync(`${dataFolder}/${solution.inputFile}`, 'utf-8');
            let start = process.hrtime();
            result = solution.solve(content);
            let duration = (process.hrtime(start)[1] / 1000000).toFixed(2);
            results.push({file, passed: true, result, duration});
        }
    }));

    results.sort((a, b) => {
        return a.file.localeCompare(b.file, undefined, {numeric: true, sensitivity: 'base'});
    });

    const red = '\x1b[31m';
    const green = '\x1b[32m';
    const reset = '\x1b[0m';
    let alt = 0;

    console.log('\nResults:');
    results.forEach(r => {
        let color = alt++ % 2 === 0 ? green : red;
        
        if (!r.passed) {
            console.error(`${color}${r.file} - Test failed. Expected (${r.expected}) but got (${r.result})${reset}`);
        }
        else {
            console.log(`${color}${r.file} - ${r.result} [${r.duration}ms]${reset}`);
        }
    });

    let totalDuration = (process.hrtime(totalStart)[1] / 1000000).toFixed(2);
    console.log(`\nTotal duration: ${totalDuration}ms`);
}

runSolutions();