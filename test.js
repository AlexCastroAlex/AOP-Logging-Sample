import http from "k6/http";
import { check, sleep } from "k6";
import { jUnit, textSummary } from 'https://jslib.k6.io/k6-summary/0.0.1/index.js';

// Common things
const API_URL = 'https://localhost:7223';

// Test setup
export let options = {
    vus: 1,
    duration: '1m'
};

// Test scenario
export default function () {


    //call simple DTO endpoint
    let callBookSimpleDTO = http.get(`${API_URL}/BookSimpleDTO`)

    check(
        callBookSimpleDTO,
        { "call simple book DTO is 200": (r) => r.status == 200 }
    );

    //call simple DTO endpoint
    let callBookFullDTO = http.get(`${API_URL}/BookFullDTO`)

    check(
        callBookFullDTO,
        { "call full book DTO is 200": (r) => r.status == 200 }
    );

    //call simple DTO endpoint
    let callGetBookFullDTOWithProject = http.get(`${API_URL}/GetBookFullDTOWithProject`)

    check(
        callGetBookFullDTOWithProject,
        { "call callGetBookFullDTOWithProject is 200": (r) => r.status == 200 }
    );

    // Short break between iterations
    sleep(0.5);
}
export function handleSummary(data) {
  let filepath = `./loadtest-result.xml`;
    return {
        'stdout': textSummary(data, { indent: ' ', enableColors: true}), 
        './loadtest-results.xml': jUnit(data), 
    }
}
