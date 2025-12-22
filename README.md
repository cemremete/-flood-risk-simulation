# Flood Risk Property Evaluation Engine

A FlexScript 21.1 application that evaluates residential properties based on flood risk criteria, budget constraints, and safety optimization metrics. The system implements a two-phase evaluation process: constraint-based screening followed by safety score optimization to recommend the optimal property.

## Overview

The Flood Risk Property Evaluation Engine analyzes residential properties to identify the best option based on:
- **Budget constraints** - Filters properties within specified purchase budget
- **Risk tolerance** - Filters properties below acceptable flood risk threshold
- **Safety optimization** - Calculates normalized safety scores based on elevation and distance from river

The system processes property data through a sequential pipeline: data initialization → parameter validation → screening → optimization → results output.

## Features

- **Hard Constraint Filtering**: Screens properties based on budget and risk tolerance
- **Normalized Safety Scoring**: Calculates weighted safety scores using min-max normalization
- **Multi-Criteria Optimization**: Combines elevation and distance metrics with equal weighting
- **Risk Categorization**: Classifies properties as LOW, MODERATE, or HIGH risk
- **Comprehensive Output**: Detailed evaluation results with formatted statistics
- **Error Handling**: Validates inputs and provides clear error messages
- **Fully Automated**: No manual table creation or external dependencies required

## Technical Specifications

- **Language**: FlexScript 21.1
- **Platform**: FlexSim Simulation Software
- **Data Structure**: Nested Arrays (embedded data)
- **Complexity**: O(n) linear time complexity
- **Dependencies**: None (fully self-contained)

## Data Structure

Properties are stored in a nested array structure:

```flexscript
Array housesData = [
    [houseNum, elevation, distance, risk, price],
    ...
];
```

**Data Format**:
- `houseNum` (int): Property identifier (1-10)
- `elevation` (double): Elevation above river in meters
- `distance` (double): Distance from river in meters
- `risk` (double): Flood risk estimate (0.0-1.0)
- `price` (double): Property price in USD

## Algorithm Overview

### Phase 1: Screening
Filters properties using hard constraints:
- **Constraint 1**: `price ≤ userBudget`
- **Constraint 2**: `risk ≤ maxRiskTolerance`

Properties must satisfy **both** constraints to be eligible for optimization.

### Phase 2: Optimization
Calculates safety scores for eligible properties:

1. **Range Calculation**: Determines min/max values for elevation and distance
2. **Normalization**: Normalizes values to 0-1 range using min-max formula
3. **Scoring**: Combines normalized metrics with equal weighting:
   ```
   safetyScore = 0.5 * elevNorm + 0.5 * distNorm
   ```
4. **Selection**: Identifies property with maximum safety score

### Risk Assessment
Properties are categorized based on risk value:
- **LOW RISK**: `risk < 0.30` - Minimal flood exposure
- **MODERATE RISK**: `0.30 ≤ risk < 0.60` - Consider additional flood insurance
- **HIGH RISK**: `risk ≥ 0.60` - Significant flood protection measures recommended

## Usage

### Prerequisites
- FlexSim 21.1 or compatible version
- Access to FlexScript execution environment

### Running the Script

1. **Open FlexSim** and navigate to the script execution environment
2. **Load the script** (`Untitled-1.fs` or your script file)
3. **Configure parameters** (optional - defaults provided):
   ```flexscript
   double userBudget = 300000;        // Maximum purchase budget
   double maxRiskTolerance = 0.40;    // Maximum acceptable risk (0.0-1.0)
   ```
4. **Execute the script** - Results will be displayed in the console

### Output Format

The script provides:
- Search parameters summary
- Individual property evaluation results
- Eligible properties count
- Safety score calculations for each eligible property
- Recommended property details
- Risk assessment categorization

**Example Output**:
```
FLOOD RISK PROPERTY EVALUATION ENGINE

Search Parameters:
  Maximum Budget: $300000
  Risk Tolerance: 0.40
  Properties to Evaluate: 10

SCREENING PHASE - Filtering by Hard Constraints
...

OPTIMIZATION PHASE - Selecting Best Property
...

SELECTION RESULTS
RECOMMENDED PROPERTY: #1
...
```

## Repository Structure

```
flood-risk-simulation/
├── README.md                          # Project overview and usage
├── docs/
│   └── architecture/
│       └── Architecture-Documentation.md  # Detailed technical documentation
├── src/
│   └── Untitled-1.fs                 # Main FlexScript implementation
└── data/
    └── HousesData.csv                # Sample property data (optional)
```

## Documentation

For detailed technical documentation, including:
- Complete system architecture
- Algorithm flowcharts
- Data structure relationships
- Error handling patterns
- Performance characteristics

See: [Architecture Documentation](docs/architecture/Architecture-Documentation.md)

## Implementation Details

### Core Functions Used
- `print()` - Console output
- `string.fromNum()` - Number formatting with precision control
- `Array.length` - Array size calculation
- `Array.push()` - Array population
- `min()` / `max()` - Value comparison for normalization
- Ternary operator `? :` - Conditional assignment for edge cases

### Key Design Decisions
1. **Embedded Data**: Data stored directly in code for portability
2. **Sequential Processing**: Linear flow for clarity and maintainability
3. **Early Returns**: Error handling via early exit pattern
4. **Normalized Scoring**: Ensures fair comparison across different scales

## Limitations

- Fixed dataset size (10 properties in current implementation)
- Static parameters (budget and risk tolerance hardcoded)
- Single optimization objective (safety score only)
- No user input mechanism (parameters must be modified in code)

## Extension Points

Potential enhancements:
- External data source integration (CSV, database)
- Dynamic parameter input mechanism
- Multiple optimization objectives
- Configurable weighting factors
- GUI interface for parameter configuration

## Performance

- **Time Complexity**: O(n) where n = number of properties
- **Space Complexity**: O(n) for eligible properties array
- **Scalability**: Handles any number of properties in array

## License

This project is part of a technical evaluation/recruitment task.

## Author

Developed as part of FlexScript 21.1 technical assessment.

---

For questions or issues, please refer to the [Architecture Documentation](docs/architecture/Architecture-Documentation.md) for detailed technical information.
