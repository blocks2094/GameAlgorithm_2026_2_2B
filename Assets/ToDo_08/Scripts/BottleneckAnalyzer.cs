using System.Collections.Generic;
using AlgoLesson8;

namespace AlgoLesson8.Student
{
    /// <summary>
    /// 활성 기계 중 분당 처리량이 가장 낮은 병목 기계를 찾습니다.
    /// </summary>
    public sealed class BottleneckAnalyzer : IBottleneckAnalyzer
    {
        public string FindBottleneck(IReadOnlyList<MachineSnapshot> machines)
        {
            if (machines == null || machines.Count == 0)
                return string.Empty;

            // TODO 13: 비활성 기계는 검사에서 제외하세요.

            // TODO 14: ItemsPerMinute가 가장 낮은 기계를 찾으세요.

            // TODO 15: 처리량이 같으면 Id가 작은 기계를 선택하세요.

            // TODO 16: 선택된 기계의 DisplayName을 반환하세요.

            return string.Empty;
        }
    }
}
