# WitEngine

**WitEngine** is a modular API designed to build flexible, extensible interpreters for automating complex tasks. With its modular architecture, WitEngine allows developers to create controllers that define variables, functions, and custom logic, enabling easy integration into various automation processes, from hardware control to workflow management.

## Features

- **Modular Controllers**: Easily create controllers for different tasks, from hardware operations to task automation.
- **Script Automation**: Write simple scripts to control complex systems, using a wide variety of supported variables and activities.
- **Real-Time Feedback**: Track the execution of your scripts in real-time, with support for event handlers.
- **Customizable**: Extendable with custom variables and functions that can be serialized and deserialized to suit your needs.

## Demo Project

The demo project includes:
- **WitEngine Core**: The core engine responsible for running scripts and loading controllers.
- **Basic Controllers**: Two example controllers are provided:
  1. **Basic Variables**: Supports `int`, `double`, `string`, and `WitColor`.
  2. **Basic Operations**: Includes core activities like loops, parallel actions, delayed actions, and the `Return` function.
- **Graphical User Interface (GUI)**: A GUI to test and visualize the capabilities of WitEngine, including running scripts and viewing real-time results.

## Getting Started

To get started with **WitEngine**, follow these steps:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/dmitrat/OutWit.git
   cd OutWit

2. **Build and Run:**
- Open the solution in Visual Studio (or your preferred IDE),
- restore NuGet packages
-  run the demo project.

3. **Explore the Demo:**
   
- Try modifying the controllers and adding your own variables and functions.

5. **Write Your Own Scripts:**

- Define your variables and activities.
- Write simple scripts using the WitEngine syntax.
