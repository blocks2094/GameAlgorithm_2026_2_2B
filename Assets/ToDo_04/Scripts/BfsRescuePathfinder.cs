using System.Collections.Generic;
using AlgoCourse.Lesson4;

namespace AlgoCourse.StudentWork
{
    public sealed class BfsRescuePathfinder : IRescuePathfinder
    {
        public PathSearchResult FindPath(
            int width,
            int height,
            GridPosition start,
            GridPosition goal,
            IReadOnlyCollection<GridPosition> blockedCells)
        {
            // TODO 01: Queue, visited, cameFrom을 생성합니다.
            // TODO 02: 시작 칸을 Queue와 visited에 넣습니다.
            // TODO 03: Queue가 빌 때까지 상하좌우 이웃을 탐색합니다.
            // TODO 04: 범위, 장애물, 방문 여부를 검사합니다.
            // TODO 05: cameFrom을 따라 최단 경로를 복원합니다.
            return new PathSearchResult(
                new List<GridPosition>(),
                new List<GridPosition>());
        }
    }
}
