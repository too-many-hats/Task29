# Leviathan

This project aims to build the most realistic emulator possible of a 1956 Univac Scientific _and_ a life-size, fully functional replica of it's console + Charactron.

## What's the Univac Scientific?

The [Univac Scientific](https://en.wikipedia.org/wiki/UNIVAC_1103) was an early vacuum tube mainframe from 1956 focused on scientific computing. A huge machine weighing 22,250kg, cost $486,140.00 a month and, at best, executed just 25,000 instructions a second. Quite the Leviathan.

The machine is notable today as the first machine to implement interrupts, and the first computer designed by Seymour Cray. This has a "Repeat" instruction, why is that interesting? Well, this machine was designed by Seymour Cray and the "Repeat" instruction looks to me like the genesis of the ideas behind vector processing, eventually leading to the Cray 1. We can see Cray gradually refining the ideas in each of his designs over the next 18 years.

![1103A](/Docs/images/1103Whole.png)

The [Charactron](https://en.wikipedia.org/wiki/Charactron) was an early CRT display for visual output, we have evidence from the BRL report that Eglin Airforce Base and Convair had Charactrons installed on their 1103s.

## Why?

It's something of a travesty that Cray's first machine hasn't been emulated when just about every other computer has, especially when the Univac Scientific is both technically and historically notable. We also have everything we need to build and verify the emulator, including listings of original software, diagnostic routines and microarchitectural documentation (essential because so much state is displayed on the console). In all my research this might be the last vacuum tube mainframe with original software that doesn't have a high quality emulator.

Creating an accurate emulator of a fairly complex early machine is an interesting technical challenge that I'll have a lot of fun with all on its own, and that's the easy part of this project.

The physical replica part comes from a large wall in my apartment that needs something on it, and I don't find paintings that interesting. So why not put a _**huge**_ mainframe console there instead? It'll spark conversation no matter the occasion with it's hundreds of blinking lights, buttons and switches. Being fully functional, guests can play with the switches and buttons, or even enter and run a short program like it's 1956. While for my two little nieces, the lights, physical buttons and switches will be irresistible and should put quite the smile on their faces (as they mess up whatever program was running ;)).

And if the photos haven't emphasised the size, this absolute **beast** of a console is nearly 2.7 meters long with 360 lights and 185 buttons _for just one of the three console panels_. My soldering iron hand is going to have some RSI by the end of this.

![1103A console](/Docs/images/consoleFront.png)
* and yes, the above picture was cropped off-center in the original brochure this picture came from. Sad face.

## Can anyone else use this?

While the emulator is cross platform and implemented as a library that can be used anywhere, the "UI" is purpose built for the physical replica. So you'd need to create your own alternative front-end to the emulator, unless of course you've got a 3m long section of wall that you'd like to put a console on.

## You'll need to wear sunglasses in your living room! There's hundreds of flashing lights.

Well, part of the replica is to get as close as possible to the incandescent bulbs originally used which are nowhere near as hash on the eyes as LEDs. They don't simply flick on and off, but brighten up or darken gradually along a gradient, starting dark red when power is applied and turning yellow the longer power is applied.

Secondly, this will use a photosensor to detect ambient light and adjust the brightness to the environment.

## The Tech

I'm no hardware genius, us software guys like to keep the physical world at arms length. Nearly everything is driven by USB GPIO boards. The emulator itself is written in C# and is cross platform. The UI project that drives the replica console is a combination of SDL and C# running on Windows. 

The hardware is a *lot* of USB GPIO boards for the buttons and switches, four LCD screens are used for the actual console lights with a laser cut aluminum sheet over the top of the LCDs. This gives us software defined indicators that can colour and gradient match original bulbs while letting us easily change brightness. It also saves a ton of soldering.

## Contributing
This project is at such an early stage that contributions aren't currently accepted until there is a more defined shape to the project.

## Licence

The project, it's code, assets and documentation are licenced under GPL v3. Some PDF and images files in the Docs directory come from third parties and remain under their original licence (if still enforceable 65+ years later).