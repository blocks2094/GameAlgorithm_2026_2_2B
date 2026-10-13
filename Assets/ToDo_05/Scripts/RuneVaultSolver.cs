using System.Collections.Generic;
using AlgoCourse.Lesson5;

namespace AlgoCourse.StudentWork
{
    public sealed class RuneVaultSolver : IRuneVaultSolver
    {
        public RuneSolveResult Solve(RunePuzzle puzzle)
        {
            // TODO 01: 선택 상태를 초기화합니다.
            // TODO 02: Search(0)을 호출합니다.
            // TODO 03: 탐색 단계와 정답을 반환합니다.
            return new RuneSolveResult(
                new List<RuneSearchStep>(),
                new List<int>());
        }

        // TODO 04: 재귀 종료 조건을 작성합니다.
        // TODO 05: 후보를 선택하고 다음 깊이를 호출합니다.
        // TODO 06: 실패하면 선택을 취소합니다.
    }
}
