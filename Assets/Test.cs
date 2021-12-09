using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteAlways]
public class Test : MonoBehaviour
{
    [SerializeField] private Transform[] m_Bones = null;
    [SerializeField] private List<BoxCollider> m_BoxColliderList = new List<BoxCollider>();
    [SerializeField] private Animator m_Animator = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        m_Animator = GetComponentInChildren<Animator>();
        var skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        var hashSet = new HashSet<Transform>();
        foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
        {
            var mesh = skinnedMeshRenderer.sharedMesh;
            var boneWeights = mesh.boneWeights;
            foreach (var bone in skinnedMeshRenderer.bones)
            {
                hashSet.Add(bone);
            }
        }

        m_Bones = hashSet.ToArray();

        foreach (var bone in m_Bones)
        {
            var boxCollider = bone.gameObject.AddComponent<BoxCollider>();
            boxCollider.size /= 20;
            m_BoxColliderList.Add(boxCollider);
        }
    }

    private void OnDisable()
    {
        foreach (var col in m_BoxColliderList)
        {
            DestroyImmediate(col);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
