using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// ���� �� ������ ���� ������Ʈ
/// </summary>
public class GameMapGenerator : MonoBehaviour
{
    // TODO Test
    public GameMapInfo m_SampleGameMapInfo;

    [Header("���� �� ��� ������")]
    public GameMapBlock m_GameBlockPrefab;

    [Header("ī�޶� ����")]
    public Camera m_Camera;

    /// <summary>
    /// ������ ���� �� ����� ��ȯ�մϴ�.
    /// </summary>
    private List<GameMapBlock> _CreateMapBlocks;

    /// <summary>
    /// ������ ��θ� ��Ÿ���ϴ�.
    /// </summary>
    public List<GameMapBlock> _OptimalPath;

    /// <summary>
    /// ������ ����� ��Ÿ���ϴ�.
    /// </summary>
    public List<GameMapBlock> _Stopovers;

    private void Awake()
    {
        // ���� �� ����
        GenerateGameMap(m_SampleGameMapInfo);

        // �ִ� ��� ���ϱ�
        List<Node> optimalPathNode = GetOptimalPath(m_SampleGameMapInfo);

        // ������ �ִܰ�� ����Ʈ�� ��ȯ�մϴ�.
        foreach (Node node in optimalPathNode)
        {
            GameMapBlock mapBlock = GetGameMapBlock(
                node.nodePosition.x,
                node.nodePosition.y,
                m_SampleGameMapInfo);

            _OptimalPath.Add(mapBlock);
        }

        // ������ ����� ����ϴ�.
        _Stopovers = GetStopoverPoints(optimalPathNode, m_SampleGameMapInfo);

        // ī�޶� ��ġ �ʱ�ȭ
        InitializeCameraPosition();

        // �������� ���Ž�ŵ�ϴ�.
        GameManager.instance.m_Stopovers = _Stopovers;
    }

    /// <summary>
    /// ���� ���� �����մϴ�
    /// </summary>
    /// <param name="gameMapInfo">���� �� ���� ��ü�� �����մϴ�.</param>
    private void GenerateGameMap(GameMapInfo gameMapInfo)
    {
        // ��ü �� ũ�⸦ ��Ÿ���ϴ�.
        Vector2 mapSizeXY = new Vector2(gameMapInfo.mapSizeX, gameMapInfo.mapSizeY) * Constants.MAP_BLOCK_SIZE;

        // �� ��� ���� ���� ��ǥ�� �����մϴ�.
        Vector2 startGeneratinXY = (mapSizeXY * -0.5f) + (Vector2.one * Constants.MAP_BLOCK_SIZE * 0.5f);

        _CreateMapBlocks = new();

        for (int y = 0; y < gameMapInfo.mapSizeY; ++y)
        {
            for (int x = 0; x < gameMapInfo.mapSizeX; ++x)
            {
                // �� ��� �ε���
                Vector2Int mapBlockIndex = new(x, y);

                // �� ����� ������ ��ġ�� �����մϴ�.
                Vector2 blockPosition = startGeneratinXY + (Constants.MAP_BLOCK_SIZE * new Vector2(x, y));

                // �� ����� �����մϴ�.
                GameMapBlock blockObject = Instantiate(m_GameBlockPrefab);

                // ��� ��ġ�� �����մϴ�.
                blockObject.node.nodePosition = new(x, y);

                // ������ �� ����� ����Ʈ�� �߰��մϴ�.
                _CreateMapBlocks.Add(blockObject);

                // �� ��� Ÿ���� �����մϴ�.
                MapBlockType mapBlockType = MapBlockType.Default;

                // �� �ε����� ���� �� ��� Ÿ���� �����մϴ�.
                if (mapBlockIndex == gameMapInfo.enemySpawnPosition) mapBlockType = MapBlockType.EnemySpawn;

                else if (mapBlockIndex == gameMapInfo.enemyTargetPosition) mapBlockType = MapBlockType.EnemyTarget;

                // �� ��� �ʱ�ȭ
                blockObject.InitializeMapBlock(mapBlockType);

                // ��ġ�� �����մϴ�.
                blockObject.transform.position = new Vector3(blockPosition.x, 0, blockPosition.y);
            }
        }

    }


    /// <summary>
    /// �ִ� ��θ� ��ȯ�մϴ�.
    /// </summary>
    /// <returns>�ִ� ��θ� ����Ʈ�� ��� ��ȯ�մϴ�. 
    /// ���� ��θ� ã�� ���ߴٸ� null �� ��ȯ�մϴ�.</returns>
    public List<Node> GetOptimalPath(GameMapInfo gameMapInfo)
    {
        // ���� ��� / ���� ���
        List<Node> openList = new();
        List<Node> closeList = new();

        // ���� ��ġ
        Vector2Int spawnPosition = gameMapInfo.enemySpawnPosition;

        // �� ��ġ
        Vector2Int goalPosition = gameMapInfo.enemyTargetPosition;

        // ���� ���� ���
        Node startNode = GetGameMapBlock(spawnPosition.x, spawnPosition.y, gameMapInfo).node;

        // �� ���� ���
        Node goalNode = GetGameMapBlock(goalPosition.x, goalPosition.y, gameMapInfo).node;

        // ���� ������ ���� ��Ͽ� �߰�
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // ���� ��带 ���� ����� ù ��° ��ҷ� �����մϴ�.
            Node currentNode = openList[0];

            // ���� ���� ����� ��� �� �� ȿ�����̶�� �ǴܵǴ� ��带 ���� �˻��մϴ�.
            for (int i = 0; i < openList.Count; i++)
            {
                // �ĺ� ��� �� ���� ��庸�� �� ����� ���� ��� ��带 ã�Ұų�
                if (openList[i].totalCost < currentNode.totalCost ||

                    // �ĺ� ��� �� ����� ����Ǵ� �� ����� ����������,
                    (openList[i].totalCost == currentNode.totalCost &&

                    // ��ǥ���� ����Ǵ� ����� �� ���� ��带 ã�Ҵٸ�
                    openList[i].heuristicToGoal < currentNode.heuristicToGoal))
                {
                    // �ش� �ĺ� ��带 ���� ���� �����մϴ�.
                    currentNode = openList[i];
                }
            }

            // ���� ��Ͽ��� ���� ���õ� ��带 �����ϸ�, ���� ��忡 �߰��մϴ�.
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            // ���� ��尡 ���������
            if (currentNode == goalNode)
            {
                // ��ȯ��ų ��� ����Ʈ
                List<Node> path = new();

                // ���� ��带 ��Ÿ���ϴ�.
                // �� ������ ���������� ����� ������ ���ʴ�� �����մϴ�.
                Node prevNode = currentNode;

                while (prevNode != null)
                {
                    // ���� ��带 ��η� �߰��մϴ�.
                    path.Insert(0, prevNode);

                    // ���� ���� �����ϱ� ���� ��� ��带 ����ϴ�.
                    prevNode = prevNode.baseNode;
                }

                // ������ ��θ� ã�����Ƿ� �ش� ��θ� ��ȯ�մϴ�.
                return path;
            }

            // ���� ����� �̿� ������ ��� ����ϴ�.
            foreach (Node neighbor in GetAdjacentNodes(currentNode, gameMapInfo))
            {
                // ���� ��Ͽ� �߰��� ����� �˻����� �ʽ��ϴ�.
                if (closeList.Contains(neighbor)) continue;

                // ���� ��Ͽ� �߰��� ����� �˻����� �ʽ��ϴ�.
                if (!openList.Contains(neighbor))
                {
                    // ��� ��带 �����մϴ�.
                    neighbor.baseNode = currentNode;

                    // ���� ��忡�� ��� ����� ���
                    neighbor.costFromStart = currentNode.costFromStart + 10;

                    // ��ǥ ��ġ���� ����Ǵ� �Ÿ��� ����մϴ�.
                    neighbor.heuristicToGoal =
                        Mathf.Abs(neighbor.nodePosition.x - currentNode.nodePosition.x) +
                        Mathf.Abs(neighbor.nodePosition.y - currentNode.nodePosition.y);

                    // ���� ��Ͽ� �߰��մϴ�.
                    openList.Add(neighbor);
                }
            }
        }

        // ��θ� ã�� ���ϸ� null �� ��ȯ.
        return null;
    }

    /// <summary>
    /// ������ ����� ����ϴ�.
    /// </summary>
    /// <param name="path">��� ������ �����մϴ�.</param>
    /// <param name="gameMapInfo">���� �� ������ �����մϴ�.</param>
    /// <returns></returns>
    public List<GameMapBlock> GetStopoverPoints(List<Node> path, GameMapInfo gameMapInfo)
    {
        // ���������� ������ ����Ʈ
        List<GameMapBlock> stopoverPoints = new();

        // ���� ��带 ��Ÿ�� ����
        Node prevNode = null;

        // ���� ��ο��� ���� ��η� �̵��ϱ� ���� ������ ��Ÿ���ϴ�.
        Vector2Int nextDirection = Vector2Int.zero;

        foreach (Node node in path)
        {
            // �� ��° ������ ����մϴ�.
            if(prevNode == null)
            {
                prevNode = node;
                continue;
            }

            // ������ ����մϴ�.
            Vector2Int direction = prevNode.nodePosition - node.nodePosition;

            // �̵��ϴ� ������ �޶����� ���, ���� ��尡 �������� �� �� �ֵ��� �մϴ�.
            if(direction != nextDirection)
            {
                // ���� ��忡 ���� ���� �� ����� ����ϴ�.
                GameMapBlock precGameMapBlock = GetGameMapBlock(
                    prevNode.nodePosition.x,
                    prevNode.nodePosition.y,
                    gameMapInfo);

                // ���� ��带 �������� �����մϴ�.
                stopoverPoints.Add(precGameMapBlock);

                // ������ �����մϴ�.
                nextDirection = direction;
            }

            // ���� ��θ� ���� ��η� �����մϴ�.
            prevNode = node;
        }

        // �������� �������� �߰��մϴ�.
        GameMapBlock goalBlock = GetGameMapBlock(
            gameMapInfo.enemyTargetPosition.x,
            gameMapInfo.enemyTargetPosition.y,
            gameMapInfo);

        // ������ ��带 ������ ��Ͽ� �߰��մϴ�.
        stopoverPoints.Add(goalBlock);

        // ������ ����� ��ȯ�մϴ�.
        return stopoverPoints;

    }

    /// <summary>
    /// ī�޶� ��ġ�� �ʱ�ȭ�մϴ�.
    /// </summary>
    private void InitializeCameraPosition()
    {
        // �� Y ũ�⸦ �̿��Ͽ� ī�޶� ���̸� ��ȯ�մϴ�.
        float GetHeightFromMapY()
        {
            float angle = 90.0f - (m_Camera.fieldOfView * 0.5f);

            float h = Mathf.Tan(angle * Mathf.Deg2Rad) * m_SampleGameMapInfo.mapSizeY * Constants.MAP_BLOCK_SIZE * 0.5f;

            return h;
        }

        // �� X ũ�⸦ �̿��Ͽ� ī�޶� ���̸� ��ȯ�մϴ�.
        float GetHeightFromMapX()
        {
            float angle = 90.0f - (m_Camera.fieldOfView * 0.5f * 0.5f);

            float h = Mathf.Tan(angle * Mathf.Deg2Rad) * m_SampleGameMapInfo.mapSizeX * Constants.MAP_BLOCK_SIZE * 0.5f;

            return h;
        }

        // ī�޶� �Ʒ��� �ٶ󺸵��� �մϴ�.
        m_Camera.transform.eulerAngles = Vector3.right * 90.0f;

        float heightFromMapY = GetHeightFromMapY();
        float heightFromMapX = GetHeightFromMapX();

        // �� ���� ���̰��� ����Ͽ� ��� ���� ���� �� �ֵ��� �մϴ�.
        float cameraHeight = (heightFromMapY > heightFromMapX) ? heightFromMapY : heightFromMapX;

        // ī�޶� ���̸� �����մϴ�.
        m_Camera.transform.position = Vector3.up * (cameraHeight + Constants.CAMERA_HEIGHT_TERM);






    }

    /// <summary>
    /// (x , y) ��ġ�� ������ GameMapBlock ��ü�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="x">����� �ϴ� GameMapBlock ��ü�� x ��ġ�� �����մϴ�.</param>
    /// <param name="y">����� �ϴ� GameMapBlock ��ü�� y ��ġ�� �����մϴ�.</param>
    /// <returns></returns>
    private GameMapBlock GetGameMapBlock(int x, int y, GameMapInfo gameMapInfo)
    {
        // �� ���� ũ��
        int mapWidthSize = gameMapInfo.mapSizeX;

        // �� ��� �ε����� ����մϴ�.
        int index = (y * mapWidthSize) + x;

        // �迭 ���� �ʰ��� ���� ������ ���� ó��
        if (_CreateMapBlocks.Count <= index) return null;

        // (x, y) �� ���� GameMapBlock ��ü�� ��ȯ�մϴ�.
        return _CreateMapBlocks[index];
    }

    /// <summary>
    /// ���� ����� ��ȿ�� ������ ã�� ��ȯ�մϴ�.
    /// </summary>
    /// <param name="pivotNode">���� ��带 �����մϴ�.</param>
    /// <returns></returns>
    private List<Node> GetAdjacentNodes(Node pivotNode, GameMapInfo gameMapInfo)
    {
        // ���� ������ Ȯ�ν�ų �����Դϴ�.
        Vector2Int[] checkDirections =
        {
            Vector2Int.left,
            Vector2Int.right,
            Vector2Int.up,
            Vector2Int.down
        };

        // �̿� ������ �����ص� ����Ʈ
        List<Node> neighbors = new();

        foreach (Vector2Int checkDirection in checkDirections)
        {
            // �˻��� ��� ��ġ�� ����ϴ�.
            Vector2Int checkPosition = pivotNode.nodePosition + checkDirection;

            // ��ȿ���� ���� ����� Ȯ������ �ʽ��ϴ�.
            if (!IsValidNode(checkPosition, gameMapInfo)) continue;

            // �˻��ϴ� ��ġ�� GameMapBlock ��ü�� ����ϴ�.
            GameMapBlock neighbor = GetGameMapBlock(checkPosition.x, checkPosition.y, gameMapInfo);

            // �ͷ��� ��ġ�� ����̹Ƿ� Ȯ������ �ʽ��ϴ�.
            if (neighbor.isTurretExist) continue;

            // �̿� ���� �߰��մϴ�.
            neighbors.Add(neighbor.node);
        }

        // �̿� ���� ��ȯ
        return neighbors;
    }

    /// <summary>
    /// ��ȿ�� ������� Ȯ���մϴ�.
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

        // ��带 �׸��ϴ�.
        float nodeSimbolRadius = 0.05f;

        foreach (GameMapBlock pathMapBlock in _OptimalPath)
        {
            Vector3 nodeDrawPosition = pathMapBlock.transform.position + (Vector3.up * nodeSimbolRadius);

            Gizmos.DrawSphere(nodeDrawPosition, nodeSimbolRadius);
        }

        // ���� �������� ������ ����
        GameMapBlock prevStopoverPoint = null;

        Vector3 LineHeight = (Vector3.up * 0.05f);

        // �������� �׸��ϴ�.
        foreach (GameMapBlock stopoverPoint in _Stopovers)
        {
            // ���� �������� null �� �ƴ� ���.
            if(prevStopoverPoint != null)
            {
                // ��θ� ������ ǥ���մϴ�.
                Gizmos.DrawLine(
                    prevStopoverPoint.transform.position + LineHeight, 
                    stopoverPoint.transform.position + LineHeight);
            }

            // ���� �������� ���� �������� �ֽ��ϴ�.
            prevStopoverPoint = stopoverPoint;
        }

    }
#endif
}
