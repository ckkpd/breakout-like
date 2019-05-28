using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

public class Ranking : MonoBehaviour
{
    public static async Task<string> LoadRanking(uint n = 10)
    {
        var scores = await Score.GetScores(n);
        // Descending sort
        scores.Sort((a, b) => b.score - a.score);

        StringBuilder sb = new StringBuilder();
        for(var i = 0; i < scores.Capacity; i++)
        {
            sb.Append($"{i+1}: {scores[i].name}, {scores[i].score}\n");
        }

        return sb.ToString();
    }
}
