using System.Collections.Generic;
using System.Linq;
using AlgoLesson7;

namespace AlgoLesson7.Student
{
    /// <summary>
    /// 목표 유닛 옆의 빈 칸까지 BFS로 이동할 첫 번째 육각칸을 찾습니다.
    /// TODO가 비어 있는 동안에는 단순히 목표와 가까워지는 인접 칸을 선택합니다.
    /// </summary>
    public sealed class HexPathfinder : IHexPathfinder
    {
        public AutoChessBoardCell FindNextStep(
            AutoChessBoardCell start,
            AutoChessBoardCell target,
            IReadOnlyList<AutoChessBoardCell> cells)
        {
            if (start == null || target == null || cells == null)
                return null;

            HashSet<AutoChessBoardCell> goals = 
                cells.Where(cell => cell.Occupant == null &&
                HexGridUtility.GetDistance(cell, target) == 1).ToHashSet();

            Queue<AutoChessBoardCell> frontire = new Queue<AutoChessBoardCell>();

            Dictionary<AutoChessBoardCell, AutoChessBoardCell> cameForm =
                new Dictionary<AutoChessBoardCell, AutoChessBoardCell>();

            frontire.Enqueue(start);
            cameForm[start] = null;
            AutoChessBoardCell reachedGoal = null;

            while(frontire.Count > 0 && reachedGoal == null)
            {
                AutoChessBoardCell current = frontire.Dequeue();

                foreach(AutoChessBoardCell neighboor in HexGridUtility.GetOpenNeighbors(current, cells))
                {
                    if (cameForm.ContainsKey(neighboor))
                        continue;

                    cameForm[neighboor] = current;

                    if(goals.Contains(neighboor))
                    {
                        reachedGoal = neighboor;
                        break;
                    }
                    frontire.Enqueue(neighboor);
                }
            }

            if (reachedGoal == null)
                return null;

            AutoChessBoardCell next = reachedGoal;

            while (cameForm[next] != start)
            {
                next = cameForm[next];
            }

            return next;
        }

        private static AutoChessBoardCell FindGreedyStep(
            AutoChessBoardCell start,
            AutoChessBoardCell target,
            IReadOnlyList<AutoChessBoardCell> cells)
        {
            AutoChessBoardCell best = null;
            int bestDistance = HexGridUtility.GetDistance(start, target);

            foreach (AutoChessBoardCell neighbor in HexGridUtility.GetOpenNeighbors(start, cells))
            {
                int distance = HexGridUtility.GetDistance(neighbor, target);

                if (distance < bestDistance)
                {
                    best = neighbor;
                    bestDistance = distance;
                }
            }

            return best;
        }
    }
}
