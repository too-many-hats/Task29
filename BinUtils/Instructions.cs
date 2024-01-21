namespace BinUtils;

public class Instructions
{
    public readonly static Instruction FloatingPolyMultiply = new("FPuv", "Floating Point Polynomial Multiply", "1");
    public readonly static Instruction FloatingInnerProduct = new("FIuv", "Floating Point Inner Product", "2");
    public readonly static Instruction FloatingUnpack = new("UPuv", "Floating Point Unpack", "3");
    public readonly static Instruction FloatingNormalisePack = new("NPuv", "Floating Point Normalize Pack", "4");
    public readonly static Instruction FloatingRound = new("FRj-", "Floating Point Round Option", "5");
    public readonly static Instruction TransmitPositive = new("TPuv", "Transmit Positive", "11");
    public readonly static Instruction TransmitMagnitude = new("TMuv", "Transmit Magnitude", "12");
    public readonly static Instruction TransmitNegative = new("TNuv", "Transmit Negative", "13");
    public readonly static Instruction Interpret = new("IP--", "Interpret", "14");
    public readonly static Instruction TransmitUAddress = new("TUuv", "Transmit U Address", "15");
    public readonly static Instruction TransmitVAddress = new("TVuv", "Transmit V Address", "16");
    public readonly static Instruction ExternalFunction = new("EF-v", "External Function", "17");
    public readonly static Instruction ReplaceAdd = new("RAuv", "Replace Add", "21");
    public readonly static Instruction LeftTransmit = new("LTjkv", "Left Transmit", "22");
    public readonly static Instruction ReplaceSubtract = new("RSuv", "Replace Subtract", "23");
    public readonly static Instruction ControlledComplement = new("CCuv", "Controlled Complement", "27");
    public readonly static Instruction SplitPositiveEntry = new("SPuk", "Split Positive Entry", "31");
    public readonly static Instruction SplitNegativeEntry = new("SNuk", "Split Negative Entry", "33");
    public readonly static Instruction SplitAdd = new("SAuk", "Split Add", "32");
    public readonly static Instruction SplitSubtract = new("SSuk", "Split Subtract", "34");
    public readonly static Instruction AddAndTransmit = new("ATuv", "Add And Transmit", "35");
    public readonly static Instruction SubtractAndTransmit = new("STuv", "Subtract And Transmit", "36");
    public readonly static Instruction ReturnJump = new("RJuv", "Return Jump", "37");
    public readonly static Instruction IndexJump = new("IJuv", "Index Jump", "41");
    public readonly static Instruction ThresholdJump = new("TJuv", "Threshold Jump", "42");
    public readonly static Instruction EqualityJump = new("EJuv", "Equality Jump", "43");
    public readonly static Instruction QJump = new("QJuv", "Q Jump", "44");
    public readonly static Instruction ManuallySelectiveJump = new("MJjv", "Manually Selective Jump", "45");
    public readonly static Instruction SignJump = new("SJuv", "Sign Jump", "46");
    public readonly static Instruction ZeroJump = new("ZJuv", "Zero Jump", "47");
    public readonly static Instruction QControlledTransmit = new("QTuv", "Q Controlled Transmit", "51");
    public readonly static Instruction QControlledAdd = new("QAuv", "Q Controlled Add", "52");
    public readonly static Instruction QControlledSubstitute = new("QSuv", "Q Controlled Substitute", "53");
    public readonly static Instruction LeftShiftInA = new("LAuk", "Left Shift In A", "54");
    public readonly static Instruction LeftShiftInQ = new("LQuk", "Left Shift In Q", "55");
    public readonly static Instruction ManuallySelectiveStop = new("MSjv", "Manually Selective Stop", "56");
    public readonly static Instruction ProgramStop = new("PS--", "Program Stop", "57");
    public readonly static Instruction Print = new("PR-v", "Print", "61");
    public readonly static Instruction Punch = new("PU-v", "Punch", "63");
    public readonly static Instruction FloatingPointAdd = new("FAuv", "Floating Point Add", "64");
    public readonly static Instruction FloatingPointSubtract = new("FSuv", "Floating Point Subtract", "65");
    public readonly static Instruction FloatingPointMultiply = new("FMuv", "Floating Point Multiply", "66");
    public readonly static Instruction FloatingPointDivide = new("FDuv", "Floating Point Divide", "67");
    public readonly static Instruction Multiply = new("MPuv", "Multiply", "71");
    public readonly static Instruction MultiplyAdd = new("MAuv", "Multiply Add", "72");
    public readonly static Instruction Divide = new("DVuv", "Divide", "73");
    public readonly static Instruction ScaleFactor = new("SFuv", "Scale Factor", "74");
    public readonly static Instruction Repeat = new("RPjnw", "Repeat", "75");
    public readonly static Instruction ExternalRead = new("ERjv", "External Read", "76");
    public readonly static Instruction ExternalWrite = new("EWjv", "External Write", "77");

    public readonly static IReadOnlyList<Instruction> All = new List<Instruction>
    {
        FloatingInnerProduct, FloatingUnpack, FloatingNormalisePack, FloatingRound, FloatingPolyMultiply, TransmitPositive,
        TransmitMagnitude, TransmitNegative, TransmitUAddress, TransmitVAddress, ExternalFunction, Interpret, ReplaceAdd, 
        LeftTransmit, ReplaceSubtract, ControlledComplement, SplitPositiveEntry, SplitAdd, SplitNegativeEntry, SplitSubtract,
        AddAndTransmit, SubtractAndTransmit, ReturnJump, IndexJump, EqualityJump, ThresholdJump, QJump, ManuallySelectiveJump,
        SignJump, ZeroJump, QControlledTransmit, QControlledAdd, QControlledSubstitute, LeftShiftInA, LeftShiftInQ, 
        ManuallySelectiveStop, ProgramStop, Print, Punch, FloatingPointAdd, FloatingPointSubtract, FloatingPointMultiply, 
        FloatingPointDivide, Multiply, MultiplyAdd, Divide, ScaleFactor, Repeat, ExternalRead, ExternalWrite
    };
}