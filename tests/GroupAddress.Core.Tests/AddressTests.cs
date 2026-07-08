namespace GroupAddress.Core.Tests;

public class AddressTests
{
    [Fact]
    public void ToString_WithMainGroup_FormatsAsThreeLevel()
    {
        var address = new Address(mainGroup: 1, middleGroup: 2, ga: 3);

        Assert.Equal("1/2/3", address.ToString());
    }

    [Fact]
    public void ToString_WithoutMainGroup_UsesPlaceholder()
    {
        var address = new Address(middleGroup: 2, ga: 3);

        Assert.Equal("x/2/3", address.ToString());
    }

    [Fact]
    public void Equals_SameValues_AreEqual()
    {
        var a = new Address(1, 2, 3);
        var b = new Address(1, 2, 3);

        Assert.True(a.Equals(b));
    }

    [Fact]
    public void Equals_DifferentMainGroup_AreNotEqual()
    {
        var a = new Address(1, 2, 3);
        var b = new Address(4, 2, 3);

        Assert.False(a.Equals(b));
    }

    [Fact]
    public void EqualsWithoutMainGroup_IgnoresMainGroupDifference()
    {
        var a = new Address(1, 2, 3);
        var b = new Address(4, 2, 3);

        Assert.True(a.EqualsWithoutMainGroup(b));
    }

    [Fact]
    public void Clone_ProducesEqualButIndependentInstance()
    {
        var original = new Address(1, 2, 3);
        var clone = original.Clone();

        Assert.Equal(original, clone);
        Assert.NotSame(original, clone);
    }
}
