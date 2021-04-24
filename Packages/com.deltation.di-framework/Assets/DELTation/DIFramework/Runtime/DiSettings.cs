﻿using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UnityEngine;

namespace DELTation.DIFramework
{
    public sealed class DiSettings : ScriptableObject
    {
        [SerializeField] private bool _showIconsInHierarchy = true;
        [SerializeField] private bool _useBakedData = true;
        [SerializeField] private string _bakedAssembliesRegex = @"^Assembly-CSharp$";

        public bool ShowIconsInHierarchy => _showIconsInHierarchy;

        public bool UseBakedData => _useBakedData;

        public bool ShouldBeBaked([NotNull] Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            var assemblyName = assembly.GetName().Name;
            var match = Regex.Match(assemblyName, _bakedAssembliesRegex);
            return match.Success;
        }

        private void OnValidate()
        {
            _bakedAssembliesRegex = _bakedAssembliesRegex?.Trim() ?? string.Empty;
        }

        public static bool TryGetInstance(out DiSettings settings)
        {
            settings = Instance;
            return settings != null;
        }

        private static DiSettings Instance
        {
            get
            {
                if (_instance) return _instance;

                _instance = LoadSettingsOrDefault();
                if (_instance) return _instance;

                _instance = CreateSettings();
                return _instance;
            }
        }

        private static DiSettings LoadSettingsOrDefault() => Resources.LoadAll<DiSettings>("").FirstOrDefault();

        private static DiSettings CreateSettings()
        {
            var settings = CreateInstance<DiSettings>();

#if UNITY_EDITOR
            if (!UnityEditor.AssetDatabase.IsValidFolder(FullFolderName))
                UnityEditor.AssetDatabase.CreateFolder(ParentFolder, Folder);

            UnityEditor.AssetDatabase.CreateAsset(settings, AssetPath);
            UnityEditor.AssetDatabase.SaveAssets();

            Debug.Log("Create new");
#endif

            return settings;
        }

        private static DiSettings _instance;
        private const string AssetPath = FullFolderName + "/DI Settings.asset";
        private const string FullFolderName = ParentFolder + "/" + Folder;
        private const string ParentFolder = "Assets";
        private const string Folder = "Resources";
    }
}