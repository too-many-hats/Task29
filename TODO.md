# TODOs

1. Indicator colour and off-on transition is now functionally working, but the "soft parts" like the exact colours and transition times are just guesses. These will need to be refined into realistic colours transitions.
1. The number of indicators, and amount of state that must be emulated and displayed on the console is shocking. At least the requirements stopped changing 60 years ago. This is basically the poster-child project for test driven development.
1. Indicator brightness calculation is far too slow. We can make UpdateIndicatorStatusEndOfCycle() store the current value of each register or flip-flop, then only when the value changes or EndFrame() is called do we calculate the state of each bit and multiple the cycles on, by the number of times the same value was updated. This should have a huge increase in performance since indicators normally carry hold the same value between cycles in the common case.