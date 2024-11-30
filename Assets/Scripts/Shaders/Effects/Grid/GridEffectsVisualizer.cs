using UnityEngine;

public class GridEffectsVisualizer : MonoBehaviour {
    public static GridEffectsVisualizer Instance { get; private set; }

    private const int k_ThreadGroupX = 8;
    private const int k_ThreadGroupY = 8;
    private const int k_ThreadGroupZ = 1;

    [SerializeField]
    private ComputeShader m_Compute;
    [SerializeField]
    private int m_ResolutionX = 18;
    [SerializeField]
    private int m_ResolutionY = 10;
    [SerializeField]
    private Transform m_Grid;
    [SerializeField]
    private SpriteRenderer m_GridRender;

    private RenderTexture m_TextureMap;

    private void Awake() {

        Instance = this;

        InitTexture();
        InitCompute();
        FillMap(false);

        SetMaterial();
    }

    private void OnDestroy() {
        DisposeTexture();
    }

    //private void Update() {
    //    if (Input.GetMouseButtonUp(1)) {
    //        UpdateMap(false, GetMouse());
    //    }
    //    else if (Input.GetMouseButtonUp(0)) {
    //        UpdateMap(true, GetMouse());
    //    }
    //}


    private void InitTexture() {
        m_TextureMap = new RenderTexture(m_ResolutionX, m_ResolutionY, 16) {
            format = RenderTextureFormat.RFloat,
            enableRandomWrite = true,
            wrapMode = TextureWrapMode.Clamp,
            filterMode = FilterMode.Point,
            useMipMap = false
        };

        m_TextureMap.Create();
    }

    private void InitCompute() {
        m_Compute.SetTexture(0, "TextureMap", m_TextureMap);
        m_Compute.SetInt("ResolutionX", m_ResolutionX);
        m_Compute.SetInt("ResolutionY", m_ResolutionY);
    }

    private void SetMaterial() {
        m_GridRender.sharedMaterial.SetTexture("_TextureMap", m_TextureMap);
        m_GridRender.sharedMaterial.SetVector("_Scale", new Vector2(m_Grid.localScale.x, m_Grid.localScale.y));
    }

    private void DisposeTexture() {
        m_TextureMap.Release();
    }

    public void UpdateMap(bool setClear, Vector2 position)
    {
        var gridPosition = TransformToGrid(position);
        m_Compute.SetBool("SetClear", setClear);
        m_Compute.SetVector("Position", gridPosition);

        m_Compute.Dispatch(0, Mathf.CeilToInt((float)m_ResolutionX / k_ThreadGroupX), Mathf.CeilToInt((float)m_ResolutionY / k_ThreadGroupY), 1);
    }

    public Vector2 GetMouse()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return pos;
    }

    private Vector2 TransformToGrid(Vector2 pos)
    {
        pos -= new Vector2(m_Grid.position.x, m_Grid.position.y);
        pos += 0.5f * new Vector2(m_Grid.localScale.x, m_Grid.localScale.y);
        return pos;
    }

    private void FillMap(bool setClear) {
        m_Compute.SetBool("SetClear", setClear);
        m_Compute.Dispatch(0, Mathf.CeilToInt((float)m_ResolutionX / k_ThreadGroupX), Mathf.CeilToInt((float)m_ResolutionY / k_ThreadGroupY), 1);
    }
}