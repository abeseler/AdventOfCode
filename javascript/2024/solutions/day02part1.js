export const inputFile = '02.input';
export const testFile = '02.sample';
export const testOutputExpected = "2";

export function solve(input) {
    let safeReports = 0;

    input.split('\n').forEach(line => {
        let report = line.split(' ').map(x => parseInt(x));
        safeReports += reportIsSafe(report) ? 1 : 0;
    });
    
    return safeReports.toString();
}

function reportIsSafe(report) {
    let direction = 0;
    for (let i = 1; i < report.length; i++) {
        let diff = report[i] - report[i - 1];
        let isWithinRange = diff !== 0 && Math.abs(diff) <= 3;
        if (!isWithinRange) {
            return false;
        }
        if (direction === 0) {
            direction = diff > 0 ? 1 : -1;
            continue;
        }
        if (direction === 1 && diff < 0) {
            return false;
        }
        if (direction === -1 && diff > 0) {
            return false;
        }
    }
    return true;
}