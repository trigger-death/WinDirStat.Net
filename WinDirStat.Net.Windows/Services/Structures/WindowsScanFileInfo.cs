﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Filesystem.Ntfs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinDirStat.Net.Model.Files;
using WinDirStat.Net.Windows.Native;

namespace WinDirStat.Net.Windows.Services.Structures {
	/// <summary>Information about a file used to populate a file item.</summary>
	public class WindowsScanFileInfo : IScanFileInfo {

		#region Fields

		/// <summary>The name of the file.</summary>
		public string Name { get; }
		/// <summary>The full path of the file.</summary>
		public string FullName { get; }
		/// <summary>The size of the file.</summary>
		public long Size { get; }
		/// <summary>The attributes of the file.</summary>
		public FileAttributes Attributes { get; }
		/// <summary>The UTC creation time of the file.</summary>
		public DateTime CreationTimeUtc { get; }
		/// <summary>The UTC last access time of the file.</summary>
		public DateTime LastAccessTimeUtc { get; }
		/// <summary>The UTC last write time of the file.</summary>
		public DateTime LastWriteTimeUtc { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs the <see cref="WindowsScanFileInfo"/> from a <see cref="Win32.Win32FindData"/>.
		/// </summary>
		internal WindowsScanFileInfo(Win32.Win32FindData find, string path) {
			Name = find.cFileName;
			FullName = path;
			Size = find.Size;
			Attributes = find.dwFileAttributes;
			CreationTimeUtc = find.CreationTimeUtc;
			LastAccessTimeUtc = find.LastAccessTimeUtc;
			LastWriteTimeUtc = find.LastWriteTimeUtc;
		}

		/// <summary>Constructs the <see cref="WindowsScanFileInfo"/> from a <see cref="INtfsNode"/>.</summary>
		internal WindowsScanFileInfo(INtfsNode node) {
			Name = node.Name;
			FullName = node.FullName;
			Size = (long) node.Size;
			Attributes = node.Attributes;
			CreationTimeUtc = node.CreationTime;
			LastAccessTimeUtc = node.LastAccessTime;
			LastWriteTimeUtc = node.LastChangeTime;
		}

		#endregion

		#region Properties

		/// <summary>Gets if the file is a directory.</summary>
		public bool IsDirectory => Attributes.HasFlag(FileAttributes.Directory);
		/// <summary>Gets if the file is a symbolic link.</summary>
		public bool IsSymbolicLink => Attributes.HasFlag(FileAttributes.ReparsePoint);

		#endregion
	}
}
