using UnityEngine;

namespace Environment.FallingTree
{
    public class FallingTree: MonoBehaviour
    {
        [SerializeField] private Collider2D treeCollider;
        [SerializeField] private Rigidbody2D rigidbody2D;
        private const int DefaultLayer = 0;
        
        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("FallingTargetPoint"))
            {
                Debug.Log("Trigger enter");
                treeCollider.gameObject.layer = DefaultLayer;
                treeCollider.gameObject.tag = "Ground";
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
    }
}