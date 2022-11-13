using ECS.Trigger;
using NaughtyAttributes;
using UnityEngine;

namespace Maze
{
    public class MazeRenderer : MonoBehaviour
    {
#region === VARIABLES ===
        [Foldout("=== ENVIRONMENT ==="), SerializeField] private Transform m_wallPrefab;
        [Foldout("=== ENVIRONMENT ==="), SerializeField] private Transform m_floorPrefab;
        [Foldout("=== ENVIRONMENT ==="), SerializeField] private Transform m_spherePrefab;
        [Foldout("=== ENVIRONMENT ==="), SerializeField] private Transform m_sphereEnvironment;

        [Space(10)]
        [Foldout("=== SIZES ==="), SerializeField] private uint m_mazeWidth = 10;
        [Foldout("=== SIZES ==="), SerializeField] private uint m_mazeHeight = 10;

        [Foldout("=== SIZES ==="), SerializeField] private float m_globalSize = 1f;
        [Foldout("=== SIZES ==="), SerializeField] private Vector3 m_mazeLocalSize = new Vector3(4f, 9f, 4f);

        [Space(10)]
        [Foldout("=== SPHERES ==="), SerializeField] private int m_sphereCountMax = 5;
        [Foldout("=== SPHERES ==="), SerializeField] private float m_sphereRadius = 0.5f;

        private int m_sphereCount;
        private float m_sphereActualRadius;
        private float m_sphereLevitationRadius = 0.25f;

        private System.Random random = new System.Random();

        private readonly Vector3 WALL_ANGLE_Y_90_DEGREES = new Vector3(0f, 90f, 0f);
#endregion

#region === DO SOME CODE ===
        private void Awake()
        {
            transform.localScale = m_mazeLocalSize;
            m_sphereActualRadius = m_sphereRadius * m_globalSize * 2f;

            var maze = MazeGenerator.Generate(m_mazeWidth, m_mazeHeight);
            Draw(maze);
            TriggerSphereCollect.SetSphereMaxCount(m_sphereCount);
        }

        private void Draw(WallState[,] maze)
        {
            var floor = Instantiate(m_floorPrefab, transform);
            floor.localScale = new Vector3(m_mazeWidth, 1f, m_mazeHeight);

            for (int i = 0; i < m_mazeWidth; i++)
            {
                for (int j = 0; j < m_mazeHeight; j++)
                {
                    var cell = maze[i, j];
                    var position = new Vector3((-m_mazeWidth / 2f + i) * m_globalSize, 0f, (-m_mazeHeight / 2f + j) * m_globalSize);

                    if (cell.HasFlag(WallState.UP))
                    {
                        var topWall = Instantiate(m_wallPrefab, transform);
                        topWall.localPosition = position + new Vector3(0, 0, m_globalSize / 2f);
                        topWall.localScale = new Vector3(m_globalSize, topWall.localScale.y, topWall.localScale.z);

                        if (SphereRandomSpawn())
                        {
                            SphereInstantitate(position + new Vector3(0f, 0f, -m_globalSize / 2f + m_sphereActualRadius / 2f));
                        }
                    }

                    if (cell.HasFlag(WallState.LEFT))
                    {
                        var leftWall = Instantiate(m_wallPrefab, transform);
                        leftWall.localPosition = position + new Vector3(-m_globalSize / 2f, 0, 0);
                        leftWall.localScale = new Vector3(m_globalSize, leftWall.localScale.y, leftWall.localScale.z);
                        leftWall.eulerAngles = WALL_ANGLE_Y_90_DEGREES;

                        if (SphereRandomSpawn())
                        {
                            SphereInstantitate(position + new Vector3(m_globalSize / 2f - m_sphereActualRadius / 2f, 0f, 0f));
                        }
                    }

                    if (j == 0)
                    {
                        if (cell.HasFlag(WallState.DOWN))
                        {
                            var bottomWall = Instantiate(m_wallPrefab, transform);
                            bottomWall.localPosition = position + new Vector3(0, 0, -m_globalSize / 2f);
                            bottomWall.localScale = new Vector3(m_globalSize, bottomWall.localScale.y, bottomWall.localScale.z);

                            if (SphereRandomSpawn())
                            {
                                SphereInstantitate(position + new Vector3(0f, 0f, m_globalSize / 2f - m_sphereActualRadius / 2f));
                            }
                        }
                    }

                    if (i == m_mazeWidth - 1)
                    {
                        if (cell.HasFlag(WallState.RIGHT))
                        {
                            var rightWall = Instantiate(m_wallPrefab, transform);
                            rightWall.localPosition = position + new Vector3(m_globalSize / 2f, 0f, 0f);
                            rightWall.localScale = new Vector3(m_globalSize, rightWall.localScale.y, rightWall.localScale.z);
                            rightWall.eulerAngles = WALL_ANGLE_Y_90_DEGREES;

                            if (SphereRandomSpawn())
                            {
                                SphereInstantitate(position + new Vector3(-m_globalSize / 2f + m_sphereActualRadius / 2f, 0f, 0f));
                            }
                        }
                    }
                }
            }
        }

        private bool SphereRandomSpawn()
        {
            if (random.Next(1, 1000) < 950)
            {
                return false;
            }

            return true;
        }

        private Transform SphereInstantitate(Vector3 position)
        {
            position += Vector3.up * m_sphereLevitationRadius / 4f;

            if (m_sphereCount < m_sphereCountMax)
            {
                var sphere = Instantiate(m_spherePrefab, transform);
                sphere.localPosition = position;
                sphere.localScale = new Vector3(sphere.localScale.x / m_mazeLocalSize.x, m_sphereRadius / (m_mazeLocalSize.y / 2f), sphere.localScale.z / m_mazeLocalSize.z);
                sphere.parent = m_sphereEnvironment;
                m_sphereCount++;

                return sphere.transform;
            }

            return null;
        }
#endregion
    }
}
