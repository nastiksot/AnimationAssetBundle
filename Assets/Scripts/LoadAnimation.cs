using System.Collections;
using System.IO;
using Models;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAnimation : MonoBehaviour
{
    private AnimationPaths animationPaths = new AnimationPaths();

    //Server prefab and animation paths
    // private string prefabAssetBundleURL = "http://10.0.1.19/windows/archer/archer.1";  
    // private string animationAssetBundleURL = "http://10.0.1.19/windows/animations/animations.1";
    
    //Local prefab and animation path
    private string prefabAssetBundleURL = "C:\\Git\\LoadAsset\\Assets\\AssetBundles\\Windows\\archer.1";
    private string animationAssetBundleURL = "C:\\Git\\LoadAsset\\Assets\\AssetBundles\\Windows\\animations.1";

    private string characterControllerPath = "CharacterController.controller";

    private string runtimeAnimatorControllerPath;
    private string prefabPath;

    private void InitializeDataPaths()
    {
        prefabPath = Path.Combine(FolderPath.ASSET_PREFAB_PATH, FolderPath.ARCHER_FOLDER_PATH, CharacterName.ARCHER_PREFAB_NAME + UnityFormats.ASSET_FORMAT);
        runtimeAnimatorControllerPath = Path.Combine(FolderPath.ASSET_PREFAB_PATH, FolderPath.ANIMATION_FOLDER_PATH, characterControllerPath);
        animationPaths.StandingIdleAnimationPath = Path.Combine(FolderPath.ASSET_PREFAB_PATH, FolderPath.ANIMATION_FOLDER_PATH, AnimationName.STANDING_IDLE_ANIMATION_DATA + UnityFormats.ANIMATION_DATA_FORMAT);
        animationPaths.RunForwardAnimationPath = Path.Combine(FolderPath.ASSET_PREFAB_PATH, FolderPath.ANIMATION_FOLDER_PATH, AnimationName.RUN_FORWARD_ANIMATION_DATA + UnityFormats.ANIMATION_DATA_FORMAT);
    }

    private IEnumerator Start()
    {
        InitializeDataPaths();

        var unityWebRequest = UnityWebRequestAssetBundle.GetAssetBundle(prefabAssetBundleURL);
        yield return unityWebRequest.SendWebRequest();

        // Get an asset from the bundle and instantiate it.
        var assetBundle = DownloadHandlerAssetBundle.GetContent(unityWebRequest);
        var loadAsset = assetBundle.LoadAssetAsync<GameObject>(prefabPath);
        yield return loadAsset;

        unityWebRequest = UnityWebRequestAssetBundle.GetAssetBundle(animationAssetBundleURL);
        yield return unityWebRequest.SendWebRequest();

        // Get an asset from the bundle and instantiate it.
        var bundleAnimation = DownloadHandlerAssetBundle.GetContent(unityWebRequest);

        var loadAssetAnimation = bundleAnimation.LoadAssetAsync<RuntimeAnimatorController>(runtimeAnimatorControllerPath);
        var loadAvatar = bundleAnimation.LoadAssetAsync<Avatar>(animationPaths.StandingIdleAnimationPath);
        var loadIdleAnimation = bundleAnimation.LoadAssetAsync<AnimationClip>(animationPaths.StandingIdleAnimationPath);
        var loadForwardRunAnimation = bundleAnimation.LoadAssetAsync<AnimationClip>(animationPaths.RunForwardAnimationPath);

        yield return loadAssetAnimation;

        if (!(Instantiate(loadAsset.asset) is GameObject archerObject)) yield break;

        var animator = archerObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = loadAssetAnimation.asset as RuntimeAnimatorController;

        animator.applyRootMotion = true;
        var layerInfo = new AnimatorStateInfo[animator.layerCount];
        for (var i = 0; i < animator.layerCount; i++)
        {
            layerInfo[i] = animator.GetCurrentAnimatorStateInfo(i);
        }


        animator.Update(0.0f);
        var animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController)
        {
            name = "Animator Override Controller",
            [AnimationName.STANDING_IDLE_ANIMATION_DATA] = loadIdleAnimation.asset as AnimationClip,
            [AnimationName.RUN_FORWARD_ANIMATION_DATA] = loadForwardRunAnimation.asset as AnimationClip
        };

        animator.Update(0.0f);
        animator.avatar = loadAvatar.asset as Avatar;
        animator.Play(layerInfo[0].fullPathHash);

        animator.runtimeAnimatorController = animatorOverrideController;
    }
}