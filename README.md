# Flood Risk Simulation Game

A FlexSim-based simulation model for studying decision-making processes under flood risk conditions, incorporating economic, psychological, and perceptual factors.

---

## Overview

This project develops a computer simulation model to study how individuals make decisions about:
- Real estate purchase relative to flood risk
- Property protection investments
- Insurance acquisition
- Emergency response during flood events
- Post-flood recovery strategies

---

## Documentation

### Architecture & Design
- [Complete Architecture Documentation](./docs/architecture/Architecture-Documentation.md)
- [System Flowchart](./docs/diagrams/system-flowchart.mmd)

---

## System Architecture

### Four-Phase Design

**Phase 1: Initialization**
- Configuration loading
- Market generation
- Player profile setup

**Phase 2: Pre-Flood Decisions**
- House selection algorithm
- Protection investments
- Insurance purchases

**Phase 3: Simulation Loop**
- Weather simulation
- Flood probability calculation
- Damage assessment
- Economic impacts

**Phase 4: Post-Flood Response**
- Repair decisions
- Risk reassessment
- Performance analysis

### Core Modules

- **GameController** - Main orchestration
- **HouseSelection** - Property search and optimization
- **FloodSimulation** - Weather and flood modeling
- **Economic** - Financial calculations
- **PlayerBehavior** - Psychological modeling
- **DataStorage** - Persistence and analytics

---

## Project Structure

```
flood-risk-simulation/
├── README.md
├── docs/
│   ├── architecture/
│   │   └── Architecture-Documentation.md
│   └── diagrams/
│       └── system-flowchart.mmd
└── src/
    ├── flexscript/
    └── data/
```

---

## Implementation Status

### Completed
- [x] Algorithm flowchart
- [x] Program architecture documentation
- [x] Module specifications
- [x] Data structure definitions

### In Progress
- [ ] FlexScript house selection algorithm
- [ ] Core module implementation

---

## Technology Stack

- **Platform:** FlexSim
- **Language:** FlexScript
- **Data:** FlexSim Tables, Excel

---

## Key Features

**House Selection Algorithm**
- Budget constraint filtering
- Risk tolerance evaluation
- Multi-criteria optimization

**Flood Simulation**
- Stochastic weather modeling
- Dynamic probability calculation
- Economic impact analysis

**Behavioral Modeling**
- Risk perception dynamics
- Psychological stress tracking
- Decision-making analysis