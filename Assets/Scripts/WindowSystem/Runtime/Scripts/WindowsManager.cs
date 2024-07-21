using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class WindowsManager
    {
        private readonly List<Window> _history = new();
        private readonly WindowsSpawner _spawner = new();
        
        public WindowsManager(Transform container) 
            => _spawner.SetContainer(container);

        public async void ShowAlone(string id, bool isRevisitable = true, Action callback = null)
        {
            Log($"start SHOW-ALONE [{id}]");

            await HideVisibleWindows();
            await ShowWindow(id, isRevisitable, callback);
            
            Log($"end SHOW-ALONE {id}");
        }

        public async void ShowOver(string id, Action callback = null)
        { 
            Log($"start SHOW-OVER [{id}]");
            
            PutPreviousWindowToSleep();
            await ShowWindow(id, false, callback);
            
            Log($"end SHOW-OVER [{id}]");
        }

        public async void ShowPrevious()
        {
            Log("start SHOW-PREVIOUS");
            
            await HidePreviousWindow();
            
            var window = _history.Last();

            switch (window.state)
            {
                case WindowState.Invisible:
                    await window.Show();
                    break;
                
                case WindowState.Dormant:
                    window.Wake();
                    break;
            }
            
            Log("start SHOW-PREVIOUS");
        }

        private async Task ShowWindow(string id, bool isRevisitable, Action callback = null)
        {
            var window = GetWindow(id);
            _history.Add(window);
            
            await window.Show(isRevisitable, callback);
        }

        private async Task Hide(Window window, bool removeFromHistory = false, Action callback = null)
        {
            await window.Hide(callback);

            if (!window.isRevisitable || removeFromHistory)
            {
                Log($"remove [{window.name}] from history");
                _history.Remove(window);
            }
        }

        private async Task HideVisibleWindows()
        {
            for (var i = _history.Count - 1; i >= 0; i--)
            {
                var window = _history.ElementAt(i);
                
                if (!window.isInvisible)
                {
                    await Hide(window);
                }
            }
        }

        private void PutPreviousWindowToSleep() 
            => _history.Last().Sleep();

        private Task HidePreviousWindow() 
            => Hide(_history.Last(), true);

        private Window GetWindow(string id) 
            => _spawner.GetWindow(id);

        public void AddPrefab(string id, Window window) 
            => _spawner.AddPrefab(id, window);

        private void Log(string message) 
            => Debug.Log($"WindowsManager {message}");
    }