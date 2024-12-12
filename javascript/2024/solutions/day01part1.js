export const inputFile = '01.input';
export const testFile = '01.sample';
export const testOutputExpected = "11";

export function solve(input) {
    let left = [];
    let right = [];

    input.split('\n').forEach(line => {
        let parts = line.split(/\s+/);
        left.push(parseInt(parts[0]));
        right.push(parseInt(parts[1]));
    });
        
    left.sort();
    right.sort();

    var distance = 0;
    for (let i = 0; i < left.length; i++) {
        distance += Math.abs(left[i] - right[i]);
    }
    
    return distance.toString();
}
