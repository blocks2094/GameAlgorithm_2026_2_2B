using AlgoCourse.Lesson5;
using System.Collections.Generic;
using UnityEditor.Searcher;

namespace AlgoCourse.StudentWork
{
    public sealed class RuneVaultSolver : IRuneVaultSolver
    {
        private RunePuzzle puzzle;
        private readonly List<int> selectedRunelds = new List<int>();
        private readonly HashSet<int> usedRunelds = new HashSet<int>();
        private readonly List<RuneSearchStep> steps = new List<RuneSearchStep>();
        private int currentSum;






        public RuneSolveResult Solve(RunePuzzle targetPuzzle)
        {
            // TODO 01: 선택 상태를 초기화합니다.
            puzzle = targetPuzzle;
            selectedRunelds.Clear();
            usedRunelds.Clear();
            steps.Clear();
            currentSum = 0;

            // TODO 02: Search(0)을 호출합니다.

            bool solved = Search(0);

            if(!solved)
            {
                steps.Add(new RuneSearchStep(RuneSearchAction.Failed, -1, 0, currentSum, "가능한 룬 조합이 찾지 못함"));
            }

            // TODO 03: 탐색 단계와 정답을 반환합니다.
            return new RuneSolveResult(
                new List<RuneSearchStep>(steps),
                solved ? new List<int>(selectedRunelds) : new List<int>());
        }

        // TODO 04: 재귀 종료 조건을 작성합니다.
        // TODO 05: 후보를 선택하고 다음 깊이를 호출합니다.
        // TODO 06: 실패하면 선택을 취소합니다.

        private bool Search(int depth)
        {
            // 모든 슬롯을 채웠다면 완성 조건을 검사
            if (depth == puzzle.SlotCount)
            {
                if (currentSum == puzzle.TargetSum)
                {
                    steps.Add(new RuneSearchStep(RuneSearchAction.Solved, -1, depth, currentSum, "모든 조건 만족함. 금고 열림!"));
                    return true;
                }

                return false;
            }

            foreach(RuneData rune in puzzle.Runes)
            {
                // 사용할 수 없는 룬은 선택하지 않고 다음 후보로 넘김 
                if(!CanChosse(rune, depth, out string rejectReason))
                {
                    steps.Add(new RuneSearchStep(RuneSearchAction.Reject, rune.Id, depth, currentSum, rejectReason));
                    continue;
                }

                // 선택한 룬은 현재 성태 반영
                Choose(rune, depth);

                // 다음 슬롯을 재귀적 탐색
                if(Search(depth - 1))
                {
                    return true;
                }

                // 실패한 선택을 정확히 취소하고 다른 후보 시도
                Cancel(rune, depth);
            }

            return false;
        }

        private bool CanChosse(RuneData rune, int depth, out string reason)
        {
            if(usedRunelds.Contains(rune.Id))
            {
                reason = $"{rune.DisplayName} : 이미 사용한 룬임";
                return false;
            }

            if(currentSum + rune.Value > puzzle.TargetSum)
            {
                reason = $"{rune.DisplayName}: 합계가 {puzzle.TargetSum}을 초과함";
                return false;
            }

            bool isLastSlot = depth == puzzle.SlotCount - 1;
            if(puzzle.RequireLightLast && isLastSlot && rune.Element != RuneElement.Light)
            {
                reason = $"빛 룬은 마지막 슬롯을 위해 남겨야 함";
                return false;
            }

            if(puzzle.ForbidFireWaterAdjacency && selectedRunelds.Count > 0)
            {
                RuneData previous = FindRune(selectedRunelds[selectedRunelds.Count - 1]);
                bool forbiddenPair =
                    previous.Element == RuneElement.Fire && rune.Element == RuneElement.Water ||
                    previous.Element == RuneElement.Water && rune.Element == RuneElement.Fire;

                if(forbiddenPair)
                {
                    reason = $"불 룬과 물 룬은 서로 붙일 수 없음";
                    return false;
                         
                }
            }

            reason = string.Empty;
            return true;
        }

        private RuneData FindRune(int runeId)
        {
            foreach(RuneData rune in puzzle.Runes)
            {
                if (rune.Id == runeId) return rune;
            }

            return null;
        }

        private void Choose(RuneData rune, int depth)
        {
            selectedRunelds.Add(rune.Id);
            usedRunelds.Add(rune.Id);
            currentSum += rune.Value;
            steps.Add(new RuneSearchStep(RuneSearchAction.Choose, rune.Id, depth, currentSum,
                $"{depth + 1}에 {rune.DisplayName} 룬은 선택함"));
        }

        private void Cancel(RuneData rune, int depth)
        {
            selectedRunelds.RemoveAt(selectedRunelds.Count - 1);
            usedRunelds.Remove(rune.Id);
            currentSum -= rune.Value;
            steps.Add(new RuneSearchStep(RuneSearchAction.Backtrack, rune.Id, depth, currentSum,
                $"{rune.DisplayName} 선택으로 해결 할 수 없음 되돌아감"));
        }
    }
}
