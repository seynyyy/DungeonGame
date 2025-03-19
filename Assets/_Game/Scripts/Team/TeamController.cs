using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Character;
using _Game.Scripts.Infrastructure;
using _Game.Scripts.Infrastructure.Entity;
using UnityEngine;

namespace _Game.Scripts.Team
{
    public class TeamController : MonoBehaviour
    {
        [SerializeField] private List<AdventurerDataSo> adventurersData;
        [SerializeField] private EntityFactory entityFactory;
        [SerializeField] private TeamView teamView;

        [HideInInspector]
        public AdventurerController selectedAdventurer; // TODO: Придумати показ команд для випадку null

        public readonly ActionContainer<Action<AdventurerController>> OnAdventurerSelected = new();
        
        private readonly ActionContainer<Action<SelectionState>> _onSelectionStateChanged = new();

        public SelectionState SelectionState { get; private set; }

        private IEnumerable<AdventurerController> GetAdventurers() => EntityRepository.GetAdventurers();
        private int GetAdventurersCount() => GetAdventurers().Count();

        public void SetSelectionState(SelectionState state)
        {
            SelectionState = state;
            _onSelectionStateChanged.Action?.Invoke(state);
        }

        public void Init()
        {
            teamView.Init(_onSelectionStateChanged);
            CreateAdventurers();
        }

        private void CreateAdventurers()
        {
            foreach (var data in adventurersData)
            {
                var adventurerController = entityFactory.CreateAdventurer(data);
                EntityRepository.Add(adventurerController);

                teamView.CreateAdventurerCardView(adventurerController);
            }
        }

        public void SelectAdventurer(AdventurerController adventurerController)
        {
            if (selectedAdventurer == adventurerController)
            {
                selectedAdventurer = null;
                SetSelectionState(SelectionState.None);
            }
            else
            {
                selectedAdventurer = adventurerController;
                SetSelectionState(SelectionState.CommandSelection);
            }

            OnAdventurerSelected.Action?.Invoke(selectedAdventurer);
        }

        public void PlaceAdventurers(List<Vector2> points)
        {
            var smoothPoints = new List<Vector2>();
            const int curveResolution = 10;

            for (var i = 0; i < points.Count - 1; i++)
            {
                var p0 = i == 0 ? points[i] : points[i - 1];
                var p1 = points[i];
                var p2 = points[i + 1];
                var p3 = i + 2 < points.Count ? points[i + 2] : points[i + 1];

                for (int j = 0; j < curveResolution; j++)
                {
                    float t = j / (float)curveResolution;
                    var point = CatmullRomInterpolate(p0, p1, p2, p3, t);
                    smoothPoints.Add(point);
                }
            }

            var totalLength = 0f;
            var placementPoints = new List<Vector2>();
            var numUnits = GetAdventurersCount();

            // 1. Обчислюємо загальну довжину кривої
            for (var i = 1; i < smoothPoints.Count; i++)
            {
                totalLength += Vector2.Distance(smoothPoints[i - 1], smoothPoints[i]);
            }

            // 2. Визначаємо відстань між юнітами
            var segmentLength = totalLength / (numUnits - 1);
            var currentDistance = 0f;

            placementPoints.Add(smoothPoints[0]); // Перша точка

            // 3. Знаходимо рівномірно розташовані точки
            for (var i = 1; i < smoothPoints.Count; i++)
            {
                currentDistance += Vector2.Distance(smoothPoints[i - 1], smoothPoints[i]);

                if (!(currentDistance >= segmentLength)) continue;
                placementPoints.Add(smoothPoints[i]);
                currentDistance = 0f;
            }

            if (placementPoints.Count < numUnits)
            {
                placementPoints.Add(smoothPoints[^1]);
            }

            // 4. Розміщуємо юнітів
            var adventurers = GetAdventurers().ToArray();
            for (int i = 0; i < Math.Min(adventurers.Length, placementPoints.Count); i++)
            {
                adventurers[i].MoveToPosition(placementPoints[i]);
            }
        }

        private static Vector2 CatmullRomInterpolate(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
        {
            var t2 = t * t;
            var t3 = t2 * t;

            return 0.5f * (
                (2 * p1) +
                (-p0 + p2) * t +
                (2 * p0 - 5 * p1 + 4 * p2 - p3) * t2 +
                (-p0 + 3 * p1 - 3 * p2 + p3) * t3
            );
        }
    }
}