using System.Collections.Generic;
using AlgoCourse.Lesson3;

namespace AlgoCourse.StudentWork
{
    public sealed class PandemicInfectionAlgorithm : ICityInfectionAlgorithm
    {
        // TODO 01: 도시 그래프, 감염 단계, 감염 요청 Queue를 생성합니다.
        // TODO 02: 한 번의 연쇄 감염에서 Outbreak한 도시를 기록합니다.
        public int PendingCount => 0;
        public int OutbreakCount { get; private set; }

        public void Initialize(IReadOnlyDictionary<int, int[]> cityGraph)
        {
            // TODO 03: 그래프를 복사하고 모든 도시의 감염 단계를 0으로 만듭니다.
        }

        public bool QueueInfection(int cityId)
        {
            // TODO 04: 존재하는 도시의 감염 요청을 Enqueue합니다.
            return false;
        }

        public CityInfectionStep ProcessNext()
        {
            // TODO 05: Dequeue 후 감염 단계를 증가시키거나 Outbreak를 처리합니다.
            return new CityInfectionStep(-1, 0, false, false);
        }

        public int GetInfectionLevel(int cityId)
        {
            // TODO 06: 도시의 현재 감염 단계를 반환합니다.
            return 0;
        }
    }
}
