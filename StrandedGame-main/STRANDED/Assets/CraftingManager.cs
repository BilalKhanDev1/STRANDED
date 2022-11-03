using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    [SerializeField] Recipe[] _recipes;

    public void TryCrafting()
    {
        int itemsInCraftingInventory = Inventory.Instance.CraftingSlot.Where(t => t.IsEmpty == false).Count();

        foreach (var recipe in _recipes)
        {
            if (IsMatchingRecipe(recipe, Inventory.Instance.CraftingSlot))
            {
                return;
            }
        }
    }

    bool IsMatchingRecipe(Recipe recipe, ItemSlot[] instanceCraftingSlot)
    {
        for (int i = 0; i <recipe.Ingrediants.Count; i++)
        {
            if (recipe.Ingrediants[i] != instanceCraftingSlot[i].Item)
                return false;
        }

        return true;
    }

    void OnValidate() => _recipes = Extensions.GetAllInstances<Recipe>();
}
