using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    internal static class CopyFileNameWithoutExtensionMenuItem
    {
        private const string MENU_ITEM_NAME = @"Assets/Kogane/Copy File Name Without Extension (Multiple) %#c";

        [MenuItem( MENU_ITEM_NAME, true )]
        private static bool CanCopy()
        {
            return Selection.assetGUIDs is { Length: > 0 };
        }

        [MenuItem( MENU_ITEM_NAME )]
        private static void Copy()
        {
            var assetGUIDs = Selection.assetGUIDs;

            if ( assetGUIDs == null || assetGUIDs.Length <= 0 ) return;

            if ( assetGUIDs.Length == 1 )
            {
                var assetPath                = AssetDatabase.GUIDToAssetPath( assetGUIDs[ 0 ] );
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension( assetPath );
                EditorGUIUtility.systemCopyBuffer = fileNameWithoutExtension;
                Debug.Log( $"Copied! `{fileNameWithoutExtension}`" );
            }
            else
            {
                var assetPaths = assetGUIDs
                        .Select( x => AssetDatabase.GUIDToAssetPath( x ) )
                        .Select( x => Path.GetFileNameWithoutExtension( x ) )
                        .OrderBy( x => x, new NaturalComparer() )
                    ;

                var result = string.Join( "\n", assetPaths );
                EditorGUIUtility.systemCopyBuffer = result;
                Debug.Log( $"Copied!\n```\n{result}\n```" );
            }
        }
    }
}