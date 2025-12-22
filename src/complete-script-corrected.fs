// ============================================================================
// FLOOD RISK HOUSE SELECTION ENGINE
// Advanced property evaluation system with multi-criteria optimization
// ============================================================================

// ---------------------------------------------------------------------------
// CONFIGURATION & VALIDATION
// ---------------------------------------------------------------------------

double userBudget = 300000;
double maxRiskTolerance = 0.40;

if (userBudget <= 0) {
    pt("ERROR: Budget must be positive");
    return 0;
}

if (maxRiskTolerance < 0 || maxRiskTolerance > 1) {
    pt("ERROR: Risk tolerance must be between 0 and 1");
    return 0;
}

// ---------------------------------------------------------------------------
// DATA LOADING & PREPARATION
// ---------------------------------------------------------------------------

Object dataTable = Table("HousesData");

if (!dataTable) {
    pt("ERROR: HousesData table not found. Please import the Excel file.");
    return 0;
}

int numRows = dataTable.numRows;
int numCols = dataTable.numCols;

if (numCols < 5) {
    pt("ERROR: Table must have at least 5 columns (house number, elevation, distance, risk, price)");
    return 0;
}

if (numRows < 2) {
    pt("ERROR: Table must have at least one data row plus header");
    return 0;
}

pt("\n============================================================================");
pt("FLOOD RISK PROPERTY EVALUATION ENGINE");
pt("============================================================================\n");
pt("Search Parameters:");
pt("  Maximum Budget: $" + numtostring(userBudget, 0, 0));
pt("  Risk Tolerance: " + numtostring(maxRiskTolerance, 1, 2));
pt("  Properties to Evaluate: " + numtostring(numRows - 1));
pt("\n============================================================================");
pt("SCREENING PHASE - Filtering by Hard Constraints");
pt("============================================================================\n");

// ---------------------------------------------------------------------------
// PHASE 1: CONSTRAINT FILTERING
// ---------------------------------------------------------------------------

Array eligibleHouses = [];
int rejectedCount = 0;

for (int i = 2; i <= numRows; i++) {
    int houseNum = i - 1;
    double elevation = gettablenum(dataTable, i, 2);
    double distance = gettablenum(dataTable, i, 3);
    double risk = gettablenum(dataTable, i, 4);
    double price = gettablenum(dataTable, i, 5);
    
    pt("----------------------------------------------------------------------------");
    pt("Property #" + numtostring(houseNum));
    pt("  Price: $" + numtostring(price, 0, 0) + " | Risk: " + numtostring(risk, 1, 2) + 
       " | Elev: " + numtostring(elevation, 0, 0) + "m | Dist: " + numtostring(distance, 0, 0) + "m");
    
    string rejectionReason = "";
    
    if (price > userBudget) {
        rejectionReason = "Price exceeds budget";
        rejectedCount++;
    } else if (risk > maxRiskTolerance) {
        rejectionReason = "Risk level too high";
        rejectedCount++;
    }
    
    if (rejectionReason != "") {
        pt("  ❌ REJECTED: " + rejectionReason);
    } else {
        pt("  ✓ ELIGIBLE: Meets all constraints");
        eligibleHouses.push([houseNum, elevation, distance, risk, price]);
    }
}

pt("----------------------------------------------------------------------------\n");

// ---------------------------------------------------------------------------
// PHASE 2: OPTIMIZATION
// ---------------------------------------------------------------------------

if (eligibleHouses.length == 0) {
    pt("============================================================================");
    pt("NO SUITABLE PROPERTIES FOUND");
    pt("============================================================================\n");
    pt("Recommendations:");
    pt("  • Increase your budget");
    pt("  • Raise risk tolerance (with caution)");
    pt("  • Wait for new listings");
    pt("\n============================================================================\n");
    return 0;
}

pt("============================================================================");
pt("OPTIMIZATION PHASE - Selecting Best Property");
pt("============================================================================\n");
pt("Eligible Properties: " + numtostring(eligibleHouses.length));
pt("Optimization Criteria:");
pt("  • Maximize elevation above river (flood protection)");
pt("  • Maximize distance from river (reduced exposure)");
pt("  • Weighted safety score calculation\n");

double minElev = 1e9;
double maxElev = -1e9;
double minDist = 1e9;
double maxDist = -1e9;

for (int i = 1; i <= eligibleHouses.length; i++) {
    Array houseData = eligibleHouses[i];
    double elev = houseData[2];
    double dist = houseData[3];
    
    minElev = fmin(minElev, elev);
    maxElev = fmax(maxElev, elev);
    minDist = fmin(minDist, dist);
    maxDist = fmax(maxDist, dist);
}

int bestHouseNum = -1;
double bestScore = -1;
Array bestHouseData = [];

pt("----------------------------------------------------------------------------");
pt("Safety Score Calculations:");
pt("----------------------------------------------------------------------------\n");

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
    
    pt("Property #" + numtostring(houseNum) + ":");
    pt("  Elevation Score: " + numtostring(elevNorm, 1, 4));
    pt("  Distance Score: " + numtostring(distNorm, 1, 4));
    pt("  Combined Safety Score: " + numtostring(safetyScore, 1, 4) + "\n");
    
    if (safetyScore > bestScore) {
        bestScore = safetyScore;
        bestHouseNum = houseNum;
        bestHouseData = houseData;
    }
}

// ---------------------------------------------------------------------------
// RESULTS PRESENTATION
// ---------------------------------------------------------------------------

pt("============================================================================");
pt("SELECTION RESULTS");
pt("============================================================================\n");
pt("Total Properties Evaluated: " + numtostring(numRows - 1));
pt("Eligible Properties: " + numtostring(eligibleHouses.length));
pt("Rejected Properties: " + numtostring(rejectedCount));
pt("\n----------------------------------------------------------------------------");
pt("★ RECOMMENDED PROPERTY: #" + numtostring(bestHouseNum));
pt("----------------------------------------------------------------------------");
pt("Optimal Safety Score: " + numtostring(bestScore, 1, 4) + "\n");

pt("Property Details:");
pt("  Purchase Price: $" + numtostring(bestHouseData[5], 0, 0));
pt("  Elevation Above River: " + numtostring(bestHouseData[2], 1, 1) + " m");
pt("  Distance from River: " + numtostring(bestHouseData[3], 0, 0) + " m");
pt("  Flood Risk Estimate: " + numtostring(bestHouseData[4], 1, 2) + "\n");

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

pt("Risk Assessment:");
pt("  Status: " + riskCategory + " - " + riskDescription + "\n");

pt("============================================================================");
pt("ANALYSIS COMPLETE");
pt("============================================================================\n");

return bestHouseNum;



