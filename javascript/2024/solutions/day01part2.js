export const inputFile = '01.input';
export const testFile = '01.sample';
export const testOutputExpected = "31";

export function solve(input) {
    let left = [];
    let right = new Map();

    input.split('\n').forEach(line => {
        let parts = line.split(/\s+/);
        left.push(parseInt(parts[0]));
        let rightValue = parseInt(parts[1]);
        if (right.has(rightValue)) {
            right.set(rightValue, right.get(rightValue) + 1);
        } else {
            right.set(rightValue, 1);
        }
    });

    var similarity = 0;
    for (let i = 0; i < left.length; i++) {
        var leftValue = left[i];
        if (right.has(leftValue)) {
            similarity += right.get(leftValue) * leftValue;
        }
    }
    
    return similarity.toString();
}
