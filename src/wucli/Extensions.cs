namespace wucli;

using System.Collections;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using WUApiLib;

public static class Extensions
{
    public static JToken ToJson(this IUpdate update)
        => new JObject
        {
            ["Title"] = update.Title,
            ["Description"] = update.Description,
            ["Categories"] = string.Join(",", update.Categories.Select(o => ((ICategory)o)?.Name)),
            ["KBArticleIDs"] = string.Join(",", update.KBArticleIDs.Select(o => (string)o)),
            ["UpdateId"] = update.Identity.UpdateID,
            ["RevisionNumber1"] = update.Identity.RevisionNumber,
            ["LastDeploymentChangeTime"] = update.LastDeploymentChangeTime,
        };

    public static IEnumerable<TResult> Select<TResult>(this IEnumerable objects, Func<object, TResult> selector)
    {
        foreach (var o in objects)
        {
            yield return selector(o);
        }
    }
}
