using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FromWithin.Enemies;
using Object = UnityEngine.Object;

namespace Tests.PlayMode.Enemies
{
    public class EnemyControllerTestSuite
    {
        private EnemyController _controller;

        [SetUp]
        public void Setup()
        {
            var testObject = Object.Instantiate(
                Resources.Load<GameObject>("Enemies/Prefabs/Golem A"));
            _controller = testObject.GetComponent<EnemyController>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.Destroy(_controller.gameObject);
        }

        [UnityTest]
        public IEnumerator TestMoveEnemyLeft()
        {
            var transform = _controller.transform;

            // Act
            var initialPosition = transform.position;
            _controller.Move(new Vector2
            {
                x = -1f,
                y = 0f
            });

            yield return new WaitForSeconds(0.1f);

            // Assert
            // ReSharper disable once Unity.InefficientPropertyAccess
            var position = transform.position;
            Assert.Less(position.x, initialPosition.x);
            Assert.AreEqual(position.y, initialPosition.y);
        }
        
        [UnityTest]
        public IEnumerator TestMoveEnemyRight()
        {
            var transform = _controller.transform;

            // Act
            var initialPosition = transform.position;
            _controller.Move(new Vector2
            {
                x = 1f,
                y = 0f
            });

            yield return new WaitForSeconds(0.1f);

            // Assert
            // ReSharper disable once Unity.InefficientPropertyAccess
            var position = transform.position;
            Assert.Greater(position.x, initialPosition.x);
            Assert.AreEqual(position.y, initialPosition.y);
        }

        [UnityTest]
        public IEnumerator TestEnemyOnlyMovesHorizontally()
        {
            // Act
            var transform = _controller.transform;
            var initialPosition = transform.position;
            _controller.Move(new Vector2
            {
                x = 1f,
                y = 0.5f
            });

            yield return new WaitForSeconds(0.1f);
            
            // Assert
            // ReSharper disable once Unity.InefficientPropertyAccess
            var position = transform.position;
            Assert.Greater(position.x, initialPosition.x);
            Assert.AreEqual(position.y, initialPosition.y);
        }

        [UnityTest]
        public IEnumerator TestEnemyJumpsWhenYVectorIsGreaterThanLimit()
        {
            // Act
            _controller.Move(new Vector2
            {
                x = 0f,
                y = 1f
            });
            yield return new WaitForSeconds(0.1f);
            
            // Assert
            Assert.True(_controller.IsJumping);
        }

        [UnityTest]
        public IEnumerator TestEnemyDoesNotJumpWhenYVectorIsLessThanLimit()
        {
            // Act
            _controller.Move(new Vector2
            {
                x = 0f,
                y = 0.1f
            });
            yield return new WaitForSeconds(0.1f);
            
            // Assert
            Assert.False(_controller.IsJumping);
        }
    }
}