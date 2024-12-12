import { readFileSync, readdirSync } from 'node:fs';
const solutionsFolder = '2024/solutions';
const dataFolder = '2024/data';

async function runSolutions() {
    const totalStart = process.hrtime();
    const files = readdirSync(solutionsFolder);

    await Promise.all(files.map(async (file) => {
        const solution = await import(`./${solutionsFolder}/${file}`);
        let content = readFileSync(`${dataFolder}/${solution.testFile}`, 'utf-8');
        let result = solution.solve(content);

        if (result !== solution.testOutputExpected) {
            console.log(`${file} - Test failed. Expected (${solution.testOutputExpected}) but got (${result})`);
        }
        else {
            content = readFileSync(`${dataFolder}/${solution.inputFile}`, 'utf-8');
            let start = process.hrtime();
            result = solution.solve(content);
            let duration = (process.hrtime(start)[1] / 1000000).toFixed(2);
            console.log(`${file} - ${result} [${duration}ms]`);
        }
    }));

    let totalDuration = (process.hrtime(totalStart)[1] / 1000000).toFixed(2);
    console.log(`\nTotal duration: ${totalDuration}ms`);
}

runSolutions();