﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace DELTation.DIFramework
{
    internal sealed class TypedCache
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AddAllObjectsTo([NotNull] ICollection<object> targetList)
        {
            if (targetList == null) throw new ArgumentNullException(nameof(targetList));

            foreach (var @object in _allObjects)
            {
                targetList.Add(@object);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet<T>(out T obj) where T : class
        {
            if (TryGet(typeof(T), out var foundObject))
            {
                obj = (T) foundObject;
                return true;
            }

            obj = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGet([NotNull] Type type, out object obj)
        {
            if (_objects.TryGetValue(type, out obj))
                return true;

            if (TryFindInSubclasses(type, out obj))
                return true;

            obj = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryFindInSubclasses([NotNull] Type type, out object obj)
        {
            foreach (var existingObject in _allObjects)
            {
                var typeOfExistingObject = existingObject.GetType();
                if (!type.IsAssignableFrom(typeOfExistingObject)) continue;

                obj = existingObject;
                _objects[type] = existingObject;
                return true;
            }

            obj = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryRegister([NotNull] object obj, out object existingObject)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var type = obj.GetType();
            if (_objects.TryGetValue(type, out existingObject))
                return false;

            _allObjects.Add(obj);
            _objects[type] = obj;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            _allObjects.Clear();
            _objects.Clear();
        }

        private readonly HashSet<object> _allObjects = new HashSet<object>();
        private readonly IDictionary<Type, object> _objects = new Dictionary<Type, object>();
    }
}