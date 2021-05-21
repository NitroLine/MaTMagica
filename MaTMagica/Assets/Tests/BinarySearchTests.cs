using System.Collections;
using System.Collections.Generic;
using Assets.MAGIC;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BinarySearchTests
{
    [Test]
    public void ItemInCombination()
    {
        var actual = new MagicHelper().BinarySearch(
            new KeyCombination(KeyCode.Q, KeyCode.E, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3), 
            KeyCode.V);
        Assert.AreEqual(actual, 0);
    }
    
    [Test]
    public void ItemNotInCombination()
    {
        var actual = new MagicHelper().BinarySearch(
            new KeyCombination(KeyCode.Q, KeyCode.E, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3), 
            KeyCode.E);
        Assert.AreEqual(actual, 1);
    }
    
    [Test]
    public void EmptyCombination()
    {
        var actual = new MagicHelper().BinarySearch(
            new KeyCombination(), 
            KeyCode.E);
        Assert.AreEqual(actual, 0);
    }
    
    [Test]
    public void ManyNeededItemsInCombination()
    {
        var actual = new MagicHelper().BinarySearch(
            new KeyCombination(KeyCode.A, KeyCode.E, KeyCode.A, KeyCode.Q, KeyCode.I, KeyCode.A), 
            KeyCode.A);
        Assert.AreEqual(actual, 1);
    }
    
    [Test]
    public void NeededIsFirst()
    {
        var actual = new MagicHelper().BinarySearch(
            new KeyCombination(KeyCode.A, KeyCode.E, KeyCode.T, KeyCode.Q, KeyCode.I, KeyCode.V), 
            KeyCode.A);
        Assert.AreEqual(actual, 1);
    }
    
    [Test]
    public void NeededIsLast()
    {
        var actual = new MagicHelper().BinarySearch(
            new KeyCombination(KeyCode.A, KeyCode.E, KeyCode.T, KeyCode.Q, KeyCode.I, KeyCode.V), 
            KeyCode.V);
        Assert.AreEqual(actual, 1);
    }
    
    [Test]
    public void IdenticalElements()
    {
        var actual = new MagicHelper().BinarySearch(
            new KeyCombination(KeyCode.A, KeyCode.A, KeyCode.A, KeyCode.A, KeyCode.A, KeyCode.A), 
            KeyCode.V);
        Assert.AreEqual(actual, 0);
    }
    
    [Test]
    public void IdenticalElementsButNotTheSame()
    {
        var actual = new MagicHelper().BinarySearch(
            new KeyCombination(KeyCode.A, KeyCode.A, KeyCode.A, KeyCode.A, KeyCode.A, KeyCode.A), 
            KeyCode.V);
        Assert.AreEqual(actual, 0);
    }
}