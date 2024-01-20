namespace Emulator;

public class Installation
{
    // Single machine cost of one of Lockheed's machines, including 10 tape units, floating point, variable blocks
    // a high-speed printer and card to tape unit. + a Charactron that Lockheed did not have. See Accuracy 1.
    public decimal MonthlyRental => 43107.36M;

    // includes the computer, small maintenance area and air-conditioning. Not including motor-generators.
    public double RequiredAreaSquareMeters => 178;

    // includes computer, air con, 250kg for Charactron
    public double WeightKg => 22245.531;

    //includes whole team, programmers, clerks, supervisors, operations, etc. Number taken from Johns Hopkins APL.
    public double RecommendedTeamSize => 28;
}