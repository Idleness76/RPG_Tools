# Townfolk Generator Detailed Requirements

From the main [Requirements](/doc/REQUIREMENTS.md) document, we have:

## Functional Requirements

1. Create random NPCs that the players encounter on their adventures. Each of the NPCs needs:

   a. A name  
   b. Physical description  
   c. Personality  
   d. Relative power dynamic

2. A method (thinking some lists?) the Feature can utilize to create names, appearances, personalites.
3. A method to faciliate NPC generation of basic in-game statistics.
4. A method to allow a user to supply details which give framework/boundaries to the NPC generation; such as profession, species, etc.
5. After an NPC is generated, a method to save that NPC's details to a file that can be accessed elsewhere.
6. If practical: A method to generate the basic information for an NPC, with suggestions for those details, and feed that to an LLM so that a more detailed and rich profile can be created.
7. Limit the "stored" choices that the application can utilize in generation an NPC to the material within the <a href="https://media.wizards.com/2016/downloads/DND/SRD-OGL_V5.1.pdf" target="_blank">D&D 5e SRD</a>
8. Design core components such that in the future other, different game systems/SRDs/resources would allow this.

## Technical Requirements

1. The components within the application should be as reusable as is practical.
2. Reliance on "external" resources, such as databases, AI models, and etc. This applicatioon should run locally on the end user's own hardware.
3. As the application grows, some capabilities may need to move into their own component areas, or even run as separate background services, if it is decided that other Features in the application could utilize those capabilities.
   1. Do not build application as microservices to start. Goal is for the code to be organized in a way that allows pieces to be pulled out later, if needed.
4. For now, this will be a local application, with a simple user interface. There's no need for a fancy graphical UI at this time.
5. Application will be built in C#, against a modern version of .NET
6. Automated tests will be added as appropriate.
7. If LLM usage is built, target a small language model that be run on the end user's hardware, rather than accessing a cloud service provider.

## NPC Generation Process

1: Gather basic NPC detail.

    a. Profession (supply or roll)
    b. Species (supply or roll)
    c. Background (tentative) (supply or roll)

2: Derive the further details possible based on 1.a and 1.b.

    a. Height
    b. Weight
    c. General appearance

3: Generate in-game statistics.

    a. Select Rolled or Templated statistics
    b. Assume level 0 commoner template
    c. Make any adjustment to ability stats based on Species

4: Generate Personality, using details from 1, 2, and 3 where applicable.

> Identified a Data Type: GameCharacter

> Identified a Resource, and an Engine: Ruleset, DiceBag respectively.

> AI augmentation will NOT be considered during the initial design.

### What is a Ruleset?

A ruleset is a collection of named lists, or other objects, which define the choices, and affects of those choices that exist with a particular game's rules.

An RPG Ruleset is a more specific version of that concept. It may include lists of charatcer species, professions, and backgrounds. Each item in those lists may modify statistics of a character which possess them.

These Rulesets will primarily manifest as Data, surrounded by Constraints; which properly classifies them as a Resource.

### What is the DiceBag, and why is it considered an Engine?

An Engine is a component of the software that DOES something; Logic, maths, etc.
The DiceBag is the preliminary title chosen for the required Random Generator Engine.
