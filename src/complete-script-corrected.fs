/**Custom Code*/
// House Selection Algorithm - FlexSim 21.1
// Selects optimal house based on budget and risk criteria

// ============================================================================
// DATA INITIALIZATION
// ============================================================================
Array housesData = [
    [1, 28, 450, 0.15, 285000],
    [2, 12, 180, 0.68, 195000],
    [3, 45, 720, 0.08, 420000],
    [4, 8, 95, 0.82, 165000],
    [5, 35, 610, 0.22, 375000],
    [6, 18, 320, 0.51, 240000],
    [7, 52, 850, 0.05, 495000],
    [8, 6, 60, 0.91, 142000],
    [9, 41, 680, 0.12, 385000],
    [10, 22, 480, 0.38, 310000]
];

int numHouses = housesData.length;

// ============================================================================
// PARAMETER VALIDATION
// ============================================================================
double userBudget = 300000;
double maxRiskTolerance = 0.40;

if (userBudget <= 0) {
    print("ERROR: Budget must be positive");
    return 0;
}

if (maxRiskTolerance < 0 || maxRiskTolerance > 1) {
    print("ERROR: Risk tolerance must be between 0 and 1");
    return 0;
}

if (numHouses == 0) {
    print("ERROR: No house data available");
    return 0;
}

// ============================================================================
// SCREENING PHASE
// ============================================================================
print("\nFLOOD RISK PROPERTY EVALUATION ENGINE\n");
print("Search Parameters:\n");
print("  Maximum Budget: $" + string.fromNum(userBudget, 0) + "\n");
print("  Risk Tolerance: " + string.fromNum(maxRiskTolerance, 2) + "\n");
print("  Properties to Evaluate: " + string.fromNum(numHouses, 0) + "\n");
print("SCREENING PHASE - Filtering by Hard Constraints\n");

Array eligibleHouses = [];
int rejectedCount = 0;

for (int i = 1; i <= numHouses; i++) {
    Array houseData = housesData[i];
    int houseNum = houseData[1];
    double elevation = houseData[2];
    double distance = houseData[3];
    double risk = houseData[4];
    double price = houseData[5];
    
    print("\nProperty #" + string.fromNum(houseNum, 0) + "\n");
    string priceStr = "  Price: $" + string.fromNum(price, 0) + " | Risk: " + string.fromNum(risk, 2);
    string elevStr = " | Elev: " + string.fromNum(elevation, 0) + "m | Dist: " + string.fromNum(distance, 0) + "m";
    print(priceStr + elevStr + "\n");
    
    string rejectionReason = "";
    
    if (price > userBudget) {
        rejectionReason = "Price exceeds budget";
        rejectedCount++;
    } else if (risk > maxRiskTolerance) {
        rejectionReason = "Risk level too high";
        rejectedCount++;
    }
    
    if (rejectionReason != "") {
        print("  REJECTED: " + rejectionReason + "\n");
    } else {
        print("  ELIGIBLE: Meets all constraints\n");
        eligibleHouses.push([houseNum, elevation, distance, risk, price]);
    }
}

print("\n");

if (eligibleHouses.length == 0) {
    print("NO SUITABLE PROPERTIES FOUND\n");
    print("Recommendations:");
    print("  - Increase your budget");
    print("  - Raise risk tolerance (with caution)");
    print("  - Wait for new listings\n");
    return 0;
}

// ============================================================================
// OPTIMIZATION PHASE
// ============================================================================
print("\nOPTIMIZATION PHASE - Selecting Best Property\n");
print("Eligible Properties: " + string.fromNum(eligibleHouses.length, 0) + "\n");
print("Optimization Criteria:\n");
print("  - Maximize elevation above river (flood protection)\n");
print("  - Maximize distance from river (reduced exposure)\n");
print("  - Weighted safety score calculation\n");
print("Safety Score Calculations:\n");

double minElev = 1e9;
double maxElev = -1e9;
double minDist = 1e9;
double maxDist = -1e9;

for (int i = 1; i <= eligibleHouses.length; i++) {
    Array houseData = eligibleHouses[i];
    double elev = houseData[2];
    double dist = houseData[3];
    
    minElev = min(minElev, elev);
    maxElev = max(maxElev, elev);
    minDist = min(minDist, dist);
    maxDist = max(maxDist, dist);
}

int bestHouseNum = -1;
double bestScore = -1;
Array bestHouseData = [];

for (int i = 1; i <= eligibleHouses.length; i++) {
    Array houseData = eligibleHouses[i];
    int houseNum = houseData[1];
    double elevation = houseData[2];
    double distance = houseData[3];
    double risk = houseData[4];
    double price = houseData[5];
    
    double elevNorm = (maxElev > minElev) ? (elevation - minElev) / (maxElev - minElev) : 1.0;
    double distNorm = (maxDist > minDist) ? (distance - minDist) / (maxDist - minDist) : 1.0;
    double safetyScore = 0.5 * elevNorm + 0.5 * distNorm;
    
    print("\nProperty #" + string.fromNum(houseNum, 0) + ":\n");
    print("  Elevation Score: " + string.fromNum(elevNorm, 4) + "\n");
    print("  Distance Score: " + string.fromNum(distNorm, 4) + "\n");
    print("  Combined Safety Score: " + string.fromNum(safetyScore, 4) + "\n");
    
    if (safetyScore > bestScore) {
        bestScore = safetyScore;
        bestHouseNum = houseNum;
        bestHouseData = houseData;
    }
}

// ============================================================================
// RESULTS OUTPUT
// ============================================================================
print("\nSELECTION RESULTS\n");
print("Total Properties Evaluated: " + string.fromNum(numHouses, 0) + "\n");
print("Eligible Properties: " + string.fromNum(eligibleHouses.length, 0) + "\n");
print("Rejected Properties: " + string.fromNum(rejectedCount, 0) + "\n");
print("RECOMMENDED PROPERTY: #" + string.fromNum(bestHouseNum, 0) + "\n");
print("Optimal Safety Score: " + string.fromNum(bestScore, 4) + "\n");

print("Property Details:\n");
print("  Purchase Price: $" + string.fromNum(bestHouseData[5], 0) + "\n");
print("  Elevation Above River: " + string.fromNum(bestHouseData[2], 1) + " m\n");
print("  Distance from River: " + string.fromNum(bestHouseData[3], 0) + " m\n");
print("  Flood Risk Estimate: " + string.fromNum(bestHouseData[4], 2) + "\n");

string riskCategory;
string riskDescription;

if (bestHouseData[4] < 0.30) {
    riskCategory = "LOW RISK";
    riskDescription = "Minimal flood exposure";
} else if (bestHouseData[4] < 0.60) {
    riskCategory = "MODERATE RISK";
    riskDescription = "Consider additional flood insurance";
} else {
    riskCategory = "HIGH RISK";
    riskDescription = "Significant flood protection measures recommended";
}

print("Risk Assessment:\n");
print("  Status: " + riskCategory + " - " + riskDescription + "\n");

print("ANALYSIS COMPLETE\n");

return bestHouseNum;
