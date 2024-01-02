using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// 게임 맵 생성을 위한 컴포넌트
/// </summary>
public class GameMapGenerator : MonoBehaviour
{
    // TODO Test
    public GameMapInfo m_SampleGameMapInfo;

    [Header("게임 맵 블록 프리팹")]
    public GameMapBlock m_GameBlockPrefab;

    [Header("카메라 변수")]
    public Camera m_Camera;

    /// <summary>
    /// 생성된 게임 맵 블록을 반환합니다.
    /// </summary>
    private List<GameMapBlock> _CreateMapBlocks;

    /// <summary>
    /// 최적의 경로를 나타냅니다.
    /// </summary>
    public List<GameMapBlock> _OptimalPath;

    /// <summary>
    /// 경유지 목록을 나타냅니다.
    /// </summary>
    public List<GameMapBlock> _Stopovers;

    private void Awake()
    {
        // 게임 맵 생성
        GenerateGameMap(m_SampleGameMapInfo);

        // 최단 경로 구하기
        List<Node> optimalPathNode = GetOptimalPath(m_SampleGameMapInfo);

        // 노드들을 최단경로 리스트로 변환합니다.
        foreach (Node node in optimalPathNode)
        {
            GameMapBlock mapBlock = GetGameMapBlock(
                node.nodePosition.x,
                node.nodePosition.y,
                m_SampleGameMapInfo);

            _OptimalPath.Add(mapBlock);
        }

        // 경유지 목록을 얻습니다.
        _Stopovers = GetStopoverPoints(optimalPathNode, m_SampleGameMapInfo);

        // 카메라 위치 초기화
        InitializeCameraPosition();

        // 경유지를 갱신시킵니다.
        GameManager.instance.m_Stopovers = _Stopovers;
    }

    /// <summary>
    /// 게임 맵을 생성합니다
    /// </summary>
    /// <param name="gameMapInfo">게임 맵 정보 객체를 전달합니다.</param>
    private void GenerateGameMap(GameMapInfo gameMapInfo)
    {
        // 전체 맵 크기를 나타냅니다.
        Vector2 mapSizeXY = new Vector2(gameMapInfo.mapSizeX, gameMapInfo.mapSizeY) * Constants.MAP_BLOCK_SIZE;

        // 맵 블록 생성 시작 좌표를 저장합니다.
        Vector2 startGeneratinXY = (mapSizeXY * -0.5f) + (Vector2.one * Constants.MAP_BLOCK_SIZE * 0.5f);

        _CreateMapBlocks = new();

        for (int y = 0; y < gameMapInfo.mapSizeY; ++y)
        {
            for (int x = 0; x < gameMapInfo.mapSizeX; ++x)
            {
                // 맵 블록 인덱스
                Vector2Int mapBlockIndex = new(x, y);

                // 맵 블록의 생성될 위치를 결정합니다.
                Vector2 blockPosition = startGeneratinXY + (Constants.MAP_BLOCK_SIZE * new Vector2(x, y));

                // 맵 블록을 생성합니다.
                GameMapBlock blockObject = Instantiate(m_GameBlockPrefab);

                // 노드 위치를 설정합니다.
                blockObject.node.nodePosition = new(x, y);

                // 생성된 맵 블록을 리스트에 추가합니다.
                _CreateMapBlocks.Add(blockObject);

                // 맵 블록 타입을 선언합니다.
                MapBlockType mapBlockType = MapBlockType.Default;

                // 맵 인덱스에 따라 맵 블록 타입을 설정합니다.
                if (mapBlockIndex == gameMapInfo.enemySpawnPosition) mapBlockType = MapBlockType.EnemySpawn;

                else if (mapBlockIndex == gameMapInfo.enemyTargetPosition) mapBlockType = MapBlockType.EnemyTarget;

                // 맵 블록 초기화
                blockObject.InitializeMapBlock(mapBlockType);

                // 위치를 설정합니다.
                blockObject.transform.position = new Vector3(blockPosition.x, 0, blockPosition.y);
            }
        }

    }


    /// <summary>
    /// 최단 경로를 반환합니다.
    /// </summary>
    /// <returns>최단 경로를 리스트에 담아 반환합니다. 
    /// 만약 경로를 찾지 못했다면 null 을 반환합니다.</returns>
    public List<Node> GetOptimalPath(GameMapInfo gameMapInfo)
    {
        // 열린 목록 / 닫힌 목록
        List<Node> openList = new();
        List<Node> closeList = new();

        // 시작 위치
        Vector2Int spawnPosition = gameMapInfo.enemySpawnPosition;

        // 끝 위치
        Vector2Int goalPosition = gameMapInfo.enemyTargetPosition;

        // 시작 지점 노드
        Node startNode = GetGameMapBlock(spawnPosition.x, spawnPosition.y, gameMapInfo).node;

        // 끝 지점 노드
        Node goalNode = GetGameMapBlock(goalPosition.x, goalPosition.y, gameMapInfo).node;

        // 시작 지점을 열린 목록에 추가
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // 현재 노드를 열린 목록의 첫 번째 요소로 설정합니다.
            Node currentNode = openList[0];

            // 만약 열린 목록의 노드 중 더 효율적이라고 판단되는 노드를 먼저 검사합니다.
            for (int i = 0; i < openList.Count; i++)
            {
                // 후보 노드 중 현재 노드보다 총 비용이 적게 드는 노드를 찾았거나
                if (openList[i].totalCost < currentNode.totalCost ||

                    // 후보 노드 중 노드의 예상되는 총 비용이 동일하지만,
                    (openList[i].totalCost == currentNode.totalCost &&

                    // 목표까지 예상되는 비용이 더 적은 노드를 찾았다면
                    openList[i].heuristicToGoal < currentNode.heuristicToGoal))
                {
                    // 해당 후보 노드를 현재 노드로 설정합니다.
                    currentNode = openList[i];
                }
            }

            // 열린 목록에서 현재 선택된 노드를 제거하며, 닫힌 노드에 추가합니다.
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            // 현재 노드가 도착지라면
            if (currentNode == goalNode)
            {
                // 반환시킬 경로 리스트
                List<Node> path = new();

                // 이전 노드를 나타냅니다.
                // 이 변수에 도착지부터 출발지 노드까지 차례대로 설정합니다.
                Node prevNode = currentNode;

                while (prevNode != null)
                {
                    // 현재 노드를 경로로 추가합니다.
                    path.Insert(0, prevNode);

                    // 현재 노드로 도달하기 위한 기반 노드를 얻습니다.
                    prevNode = prevNode.baseNode;
                }

                // 최적의 경로를 찾았으므로 해당 경로를 반환합니다.
                return path;
            }

            // 현재 노드의 이웃 노드들을 모두 얻습니다.
            foreach (Node neighbor in GetAdjacentNodes(currentNode, gameMapInfo))
            {
                // 닫힌 목록에 추가된 노드라면 검사하지 않습니다.
                if (closeList.Contains(neighbor)) continue;

                // 열린 목록에 추가된 노드라면 검사하지 않습니다.
                if (!openList.Contains(neighbor))
                {
                    // 기반 노드를 설정합니다.
                    neighbor.baseNode = currentNode;

                    // 시작 노드에서 드는 비용을 계산
                    neighbor.costFromStart = currentNode.costFromStart + 10;

                    // 목표 위치까지 예상되는 거리를 계산합니다.
                    neighbor.heuristicToGoal =
                        Mathf.Abs(neighbor.nodePosition.x - currentNode.nodePosition.x) +
                        Mathf.Abs(neighbor.nodePosition.y - currentNode.nodePosition.y);

                    // 열린 목록에 추가합니다.
                    openList.Add(neighbor);
                }
            }
        }

        // 경로를 찾지 못하면 null 을 반환.
        return null;
    }

    /// <summary>
    /// 경유지 목록을 얻습니다.
    /// </summary>
    /// <param name="path">경로 노드들을 전달합니다.</param>
    /// <param name="gameMapInfo">게임 맵 정보를 전달합니다.</param>
    /// <returns></returns>
    public List<GameMapBlock> GetStopoverPoints(List<Node> path, GameMapInfo gameMapInfo)
    {
        // 경유지들을 저장할 리스트
        List<GameMapBlock> stopoverPoints = new();

        // 이전 노드를 나타낼 변수
        Node prevNode = null;

        // 이전 경로에서 다음 경로로 이동하기 위한 방향을 나타냅니다.
        Vector2Int nextDirection = Vector2Int.zero;

        foreach (Node node in path)
        {
            // 두 번째 노드부터 계산합니다.
            if(prevNode == null)
            {
                prevNode = node;
                continue;
            }

            // 방향을 계산합니다.
            Vector2Int direction = prevNode.nodePosition - node.nodePosition;

            // 이동하는 방향이 달라지는 경우, 이전 노드가 경유지가 될 수 있도록 합니다.
            if(direction != nextDirection)
            {
                // 이전 노드에 대한 게임 맵 블록을 얻습니다.
                GameMapBlock precGameMapBlock = GetGameMapBlock(
                    prevNode.nodePosition.x,
                    prevNode.nodePosition.y,
                    gameMapInfo);

                // 이전 노드를 경유지로 설정합니다.
                stopoverPoints.Add(precGameMapBlock);

                // 방향을 갱신합니다.
                nextDirection = direction;
            }

            // 현재 경로를 이전 경로로 설정합니다.
            prevNode = node;
        }

        // 도착지를 경유지에 추가합니다.
        GameMapBlock goalBlock = GetGameMapBlock(
            gameMapInfo.enemyTargetPosition.x,
            gameMapInfo.enemyTargetPosition.y,
            gameMapInfo);

        // 도착지 노드를 경유지 목록에 추가합니다.
        stopoverPoints.Add(goalBlock);

        // 경유지 목록을 반환합니다.
        return stopoverPoints;

    }

    /// <summary>
    /// 카메라 위치를 초기화합니다.
    /// </summary>
    private void InitializeCameraPosition()
    {
        // 맵 Y 크기를 이용하여 카메라 높이를 반환합니다.
        float GetHeightFromMapY()
        {
            float angle = 90.0f - (m_Camera.fieldOfView * 0.5f);

            float h = Mathf.Tan(angle * Mathf.Deg2Rad) * m_SampleGameMapInfo.mapSizeY * Constants.MAP_BLOCK_SIZE * 0.5f;

            return h;
        }

        // 맵 X 크기를 이용하여 카메라 높이를 반환합니다.
        float GetHeightFromMapX()
        {
            float angle = 90.0f - (m_Camera.fieldOfView * 0.5f * 0.5f);

            float h = Mathf.Tan(angle * Mathf.Deg2Rad) * m_SampleGameMapInfo.mapSizeX * Constants.MAP_BLOCK_SIZE * 0.5f;

            return h;
        }

        // 카메라가 아래를 바라보도록 합니다.
        m_Camera.transform.eulerAngles = Vector3.right * 90.0f;

        float heightFromMapY = GetHeightFromMapY();
        float heightFromMapX = GetHeightFromMapX();

        // 더 높은 높이값을 사용하여 모든 맵이 보일 수 있도록 합니다.
        float cameraHeight = (heightFromMapY > heightFromMapX) ? heightFromMapY : heightFromMapX;

        // 카메라 높이를 설정합니다.
        m_Camera.transform.position = Vector3.up * (cameraHeight + Constants.CAMERA_HEIGHT_TERM);






    }

    /// <summary>
    /// (x , y) 위치의 생성된 GameMapBlock 객체를 반환합니다.
    /// </summary>
    /// <param name="x">얻고자 하는 GameMapBlock 객체의 x 위치를 전달합니다.</param>
    /// <param name="y">얻고자 하는 GameMapBlock 객체의 y 위치를 전달합니다.</param>
    /// <returns></returns>
    private GameMapBlock GetGameMapBlock(int x, int y, GameMapInfo gameMapInfo)
    {
        // 맵 가로 크기
        int mapWidthSize = gameMapInfo.mapSizeX;

        // 맵 블록 인덱스를 계산합니다.
        int index = (y * mapWidthSize) + x;

        // 배열 범위 초과에 대한 간단한 예외 처리
        if (_CreateMapBlocks.Count <= index) return null;

        // (x, y) 에 대한 GameMapBlock 객체를 반환합니다.
        return _CreateMapBlocks[index];
    }

    /// <summary>
    /// 기준 노드의 유효한 노드들을 찾아 반환합니다.
    /// </summary>
    /// <param name="pivotNode">기준 노드를 전달합니다.</param>
    /// <returns></returns>
    private List<Node> GetAdjacentNodes(Node pivotNode, GameMapInfo gameMapInfo)
    {
        // 기준 노드부터 확인시킬 방향입니다.
        Vector2Int[] checkDirections =
        {
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.up,
            Vector2Int.down
        };

        // 이웃 노드들을 저장해둘 리스트
        List<Node> neighbors = new();

        foreach (Vector2Int checkDirection in checkDirections)
        {
            // 검사할 노드 위치를 얻습니다.
            Vector2Int checkPosition = pivotNode.nodePosition + checkDirection;

            // 유효하지 않은 노드라면 확인하지 않습니다.
            if (!IsValidNode(checkPosition, gameMapInfo)) continue;

            // 검사하는 위치의 GameMapBlock 객체를 얻습니다.
            GameMapBlock neighbor = GetGameMapBlock(checkPosition.x, checkPosition.y, gameMapInfo);

            // 터렛이 배치된 블록이므로 확인하지 않습니다.
            if (neighbor.isTurretExist) continue;

            // 이웃 노드로 추가합니다.
            neighbors.Add(neighbor.node);
        }

        // 이웃 노드들 반환
        return neighbors;
    }

    /// <summary>
    /// 유효한 노드인지 확인합니다.
    /// </summary>
    /// <param name="nodePosition"></param>
    /// <returns></returns>
    private bool IsValidNode(Vector2Int nodePosition, GameMapInfo gameMapInfo)
    {
        if (nodePosition.x < 0) return false;
        else if (nodePosition.y < 0) return false;
        else if (nodePosition.x >= gameMapInfo.mapSizeX) return false;
        else if (nodePosition.y >= gameMapInfo.mapSizeY) return false;

        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (_OptimalPath == null) return;

        // 노드를 그립니다.
        float nodeSimbolRadius = 0.05f;

        foreach (GameMapBlock pathMapBlock in _OptimalPath)
        {
            Vector3 nodeDrawPosition = pathMapBlock.transform.position + (Vector3.up * nodeSimbolRadius);

            Gizmos.DrawSphere(nodeDrawPosition, nodeSimbolRadius);
        }

        // 이전 경유지를 저장할 변수
        GameMapBlock prevStopoverPoint = null;

        Vector3 LineHeight = (Vector3.up * 0.05f);

        // 경유지를 그립니다.
        foreach (GameMapBlock stopoverPoint in _Stopovers)
        {
            // 이전 경유지가 null 이 아닐 경우.
            if(prevStopoverPoint != null)
            {
                // 경로를 선으로 표시합니다.
                Gizmos.DrawLine(
                    prevStopoverPoint.transform.position + LineHeight, 
                    stopoverPoint.transform.position + LineHeight);
            }

            // 이전 경유지에 현재 경유지를 넣습니다.
            prevStopoverPoint = stopoverPoint;
        }

    }
#endif
}
