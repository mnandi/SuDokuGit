using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Microsoft.Xna.Framework.Storage;

namespace SuDoku {
	class ArchiveData {
		public ArchiveData(){
		}
		public void SaveArchive<T>(T obj) where T: class {
			/*
			// Open a storage container.
			IAsyncResult result = device.BeginOpenContainer("StorageDemo", null, null);

			// Wait for the WaitHandle to become signaled.
			result.AsyncWaitHandle.WaitOne();

			StorageContainer container = device.EndOpenContainer(result);

			// Close the wait handle.
			result.AsyncWaitHandle.Close();
			string filename = "savegame.sav";

			// Check to see whether the save exists.
			if (container.FileExists(filename))
				// Delete it so that we can create one fresh.
				container.DeleteFile(filename);
			// Create the file.
			Stream stream = container.CreateFile(filename);
			// Convert the object to XML data and put it in the stream.
			XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));

			serializer.Serialize(stream, data);
			// Close the file.
			stream.Close();
			// Dispose the container, to commit changes.
			container.Dispose();
			*/
		}
		public T LoadArchive<T>() where T: class {
			T objResult=null;
			/*
			// Open a storage container.
			IAsyncResult result =   device.BeginOpenContainer("StorageDemo", null, null);

			// Wait for the WaitHandle to become signaled.
			result.AsyncWaitHandle.WaitOne();

			StorageContainer container = device.EndOpenContainer(result);

			// Close the wait handle.
			result.AsyncWaitHandle.Close();
			string filename = "savegame.sav";

			// Check to see whether the save exists.
			if (!container.FileExists(filename)){
				// If not, dispose of the container and return.
				container.Dispose();
				return;
			}
			// Open the file.
			Stream stream = container.OpenFile(filename, FileMode.Open);
			XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
			SaveGameData data = (SaveGameData)serializer.Deserialize(stream);
			// Close the file.
			stream.Close();
			// Dispose the container.
			container.Dispose();
			*/
			return objResult;
		}
	}
}
