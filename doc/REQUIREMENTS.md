# Requirements

Document is in 2 sections. FUNCTIONAL requirements, and TECHNICAL requirements.

**Links**

- [Feature Wish List](#feature-wish-list)
- [Functional Requirements](#functional-requirements)
- [Technical Requirements](#technical-requirements)
- [Project Overview](../README.md)

## Feature Wish List

| Feature                                   | Short Description                                                            | Priority       |
| ----------------------------------------- | ---------------------------------------------------------------------------- | -------------- |
| [Townfolk Generator](#townfolk-generator) | Randomly generate some details about NPCs that the party encounters          | 1 . MVP        |
| Initiative Tracking                       | A tool to store the encounter characters and have their initiative tracked   | 2 . likely     |
| Random Encounter Sim                      | Generate an applicable/useful random party encounter based on several inputs | 3 . aspiration |

## Functional Requirements

_**What does the first tool need to do?**_

### Townfolk Generator

1. Create random NPCs that the players encounter on their adventures. Each of the NPCs needs:

   1. A name
   2. Physical description
   3. Personality
   4. Relative power dynamic

2. A method (thinking some lists?) the Feature can utilize to create names, appearances, personalites.
3. A method to faciliate NPC generation of basic in-game statistics.
4. A method to allow a user to supply details which give framework/boundaries to the NPC generation; such as profession, species, etc.
5. After an NPC is generated, a method to save that NPC's details to a file that can be accessed elsewhere.
6. If practical: A method to generate the basic information for an NPC, with suggestions for those details, and feed that to an LLM so that a more detailed and rich profile can be created.

## Technical Requirements

_**How does it do those things, and what other geeky stuff matters for this?**_

1. The components within the application should be as reusable as is practical.
2. Reliance on "external" resources, such as databases, AI models, and etc. This applicatioon should run locally on the end user's own hardware.
3. As the application grows, some capabilities may need to move into their own component areas, or even run as separate background services, if it is decided that other Features in the application could utilize those capabilities.
   1. Do not build application as microservices to start. Goal is for the code to be organized in a way that allows pieces to be pulled out later, if needed.
4. For now, this will be a local application, with a simple user interface. There's no need for a fancy graphical UI at this time.
5. Application will be built in C#, against a modern version of .NET
6. Automated tests will be added as appropriate.
7. If LLM usage is built, target a small language model that be run on the end user's hardware, rather than accessing a cloud service provider.
