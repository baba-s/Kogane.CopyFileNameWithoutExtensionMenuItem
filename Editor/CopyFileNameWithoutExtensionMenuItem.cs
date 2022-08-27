using System.IO;
using UnityEditor;
using UnityEditor.ShortcutManagement;
using UnityEngine;

namespace Kogane.Internal
{
    internal static class CopyFileNameWithoutExtensionMenuItem
    {
        [Shortcut( "Kogane/Copy File Name Without Extension", KeyCode.C, ShortcutModifiers.Action | ShortcutModifiers.Shift )]
        private static void Copy()
        {
            var activeObject = Selection.activeObject;

            if ( activeObject == null ) return;

            var assetPath = AssetDatabase.GetAssetPath( activeObject );

            if ( string.IsNullOrWhiteSpace( assetPath ) ) return;

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension( assetPath );

            EditorGUIUtility.systemCopyBuffer = fileNameWithoutExtension;

            Debug.Log( $"Copied! `{fileNameWithoutExtension}`" );
        }
    }
}