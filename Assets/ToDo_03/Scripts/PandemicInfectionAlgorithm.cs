using System.Collections.Generic;
using AlgoCourse.Lesson3;

namespace AlgoCourse.StudentWork
{
    public sealed class PandemicInfectionAlgorithm : ICityInfectionAlgorithm
    {
        private const int MaximumInfectionLevel = 3;    // 한 도시가 최대 감염 3단계

        private readonly Dictionary<int, int[]> graph = new Dictionary<int, int[]>();   // 이웃 도시 목록을 저장

        private readonly Dictionary<int, int> infectionLevels = new Dictionary<int, int>(); // 각 도시별 현재 감염 단계 저장

        private readonly Queue<int> infectionQueue = new Queue<int>();      // 감염 처리를 도시 순서대로 큐 저장

        private readonly HashSet<int> outbreakCities = new HashSet<int>();  // 이미 아웃브레이크가 발생한 도시 저장

        public int PendingCount => infectionQueue.Count;        // 감염 큐에 대기 중인 도시 개수 변환
        public int OutbreakCount { get; private set; }          // 지금까지 발생한 아웃브레이크 횟수 저장


        public void Initialize(IReadOnlyDictionary<int, int[]> cityGraph)
        {
            graph.Clear();
            infectionLevels.Clear();
            infectionQueue.Clear();
            outbreakCities.Clear();

            OutbreakCount = 0;

            // 전달 받은 모든 도시 정보를 하나씩 확인
            foreach(KeyValuePair<int, int[]> city in cityGraph)
            {
                graph.Add(city.Key, city.Value);    // 해당 도시의 이웃 도시 목록을 그래프로 저장
                infectionLevels.Add(city.Key, 0);   // 도시의 초기 감염 단계를 0으로 설정
            }
        }

        public bool QueueInfection(int cityId)      // 지정 도시를 감염 처리 대기 큐에 추가
        {
            if(!graph.ContainsKey(cityId))
            {
                return false;
            }

            if(infectionQueue.Count == 0)
            {
                outbreakCities.Clear(); 

            }

            infectionQueue.Enqueue(cityId); // 지정한 도시 감염 처리 대기 큐 마지막에 추가

            return true;
        }

        public CityInfectionStep ProcessNext()
        {

            if(infectionQueue.Count == 0)
            {
                return new CityInfectionStep(-1, 0, false, false);  // 처리할 도시가 없다면 결과 변환한다
            }

            int cityId = infectionQueue.Dequeue();      // 큐에서 가장 먼저 들어온 도시 하나를 꺼냄
            int currentLevel = infectionLevels[cityId];


            if(currentLevel < MaximumInfectionLevel)
            {
                int nextLevel = currentLevel + 1;
                infectionLevels[cityId] = nextLevel;
                return new CityInfectionStep(cityId, nextLevel, false, true);       // 감염 단게 증가 했고 아웃 브레이크 발생하지 않았다는 결과 변환
            }

            if(!outbreakCities.Add(cityId))
            {
                return new CityInfectionStep(cityId, currentLevel, false, false);   // 발생한 도시람녀 추가 확산 없다
            }

            OutbreakCount++;

            foreach(int neighborld in graph[cityId])
            {
                infectionQueue.Enqueue(neighborld);
            }

            return new CityInfectionStep(cityId, currentLevel, true, true);
        }

        public int GetInfectionLevel(int cityId)
        {
            return infectionLevels.TryGetValue(cityId, out int level) ? level : 0;  // 도시가 존재하면 감염단게를 반환하고 존재하지 않으면 0으로 변환
        }
    }
}
