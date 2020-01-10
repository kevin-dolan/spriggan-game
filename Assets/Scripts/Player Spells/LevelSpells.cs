using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



/* Initially created by Kevin Dolan on 10/15.
 * The purpose of this scriptable object is to make it safer to store data about what spells can be used and how many times in a level without leaving them in a prefab's editor window.
 * An example of an instance of this ScriptableObject: 
 * 0 : 1, 1 : 3
 * Which means that the first kind of spell (FireBlast) can be cast once and the second type of spell (IceCreate) can be cast three times. 
 * The ordering of the spells by their position in the array are determined by the enum spellType.
 */

    //made public because a getter in SpellManager could not make a getter for spellType variable
public enum SpellType { FireBlast, IceCreate, WindThrow } //enum for the spells.

[CreateAssetMenu] //this lets the ScriptableObject be created from the Create menu in the unity editor
public class LevelSpells : ScriptableObject
{
    //this list of ints represents which spells can be used (denoted by position in list) and HOW many times they can be used in a level (denoted by value of the position in the list).
    //[Header("Header header header")] //don't worry about this line, it was just me messing around with unity's inspector window
    public List<int> usableSpells; 


}
