#  Unity DI Framework

[![Version](https://img.shields.io/github/v/release/Delt06/di-framework?sort=semver)](https://github.com/Delt06/di-framework/releases)
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/48d8273db00a4d93a124ed4e6736d729)](https://www.codacy.com/gh/Delt06/di-framework/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Delt06/di-framework&amp;utm_campaign=Badge_Grade)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)


[![Tests](https://github.com/Delt06/di-framework/actions/workflows/tests.yml/badge.svg)](https://github.com/Delt06/di-framework/actions/workflows/tests.yml)

A simple Unity framework to inject dependencies into your components. The project contains a simple demo game to demonstrate the framework in action.

> Developed and tested with Unity 2019.4.17f1 LTS

## Table of contents
- [Key Features](#key-features-and-concepts)
- [Installation](#installation)
- [Setting Up](#setting-up)
- [Projects using DI Framework](#projects-using-di-framework)
- [Documentation](#documentation)

## Key Features and Concepts
- Simple and consistent injection mechanisms
    -  Method injection for `MonoBehaviour`s (through all suitable methods named `Construct`)
    -  Constructor injection for standard C# classes (a.k.a. POCO)
- Can inject anytime, not only on scene start
- The framework is designed for writing code without direct dependencies on the framework itself. You will not have to include framework's namespaces everywhere.
- Context-aware injection: dependencies can be automatically fulfilled with components found in children and parents
- Baking: to avoid reflection (and to improve performance), injection can be baked via automatic code generation
- General-purpose API: `Di` class contains convenience methods that provide access to explicit injection and object instantiation

## Installation
### Option 1
- Open Package Manager through Window/Package Manager
- Click "+" and choose "Add package from git URL..."
- Insert the URL: https://github.com/Delt06/di-framework.git?path=Packages/com.deltation.di-framework

### Option 2  
Add the following line to `Packages/manifest.json`:
```
"com.deltation.di-framework": "https://github.com/Delt06/di-framework.git?path=Packages/com.deltation.di-framework",
```

## Setting up
- Create a `GameObject` and attach a `Root Dependency Container` component.
- Using the menu of the attached `Root Dependency Container` (or manually), add the other container types. For detailed info on them, refer to the section [Container types](https://github.com/Delt06/di-framework/wiki/Containers).
- Define an injectable component according to the rules described in the section [Injection rules](https://github.com/Delt06/di-framework/wiki/Injection-Rules).
- Attach the created component to a `GameObject` and add a `Resolver` to it. Configure the `Resolver`, if needed: refer to [Resolvers](https://github.com/Delt06/di-framework/wiki/Resolver).

Example:
- Components structure:  
![Resolver Example](https://github.com/Delt06/di-framework/blob/master/Screenshots/resolver_example.jpg?raw=true)
- `Movement.cs`: 
```c#
using UnityEngine;

public sealed class Movement : MonoBehaviour
{
    public void Construct(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody;
    }

    private Rigidbody _rigidbody;
}
```

## Projects using DI Framework
- https://github.com/Delt06/fps-roguelike

## Documentation
- [Wiki](https://github.com/Delt06/di-framework/wiki)  
- [API](https://delt06.github.io/di-framework/)
