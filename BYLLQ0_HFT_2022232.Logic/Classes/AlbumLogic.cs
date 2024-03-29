﻿using BYLLQ0_HFT_2022232.Models;
using BYLLQ0_HFT_2022232.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BYLLQ0_HFT_2022232.Logic.ArtistLogic;

namespace BYLLQ0_HFT_2022232.Logic
{
    public class AlbumLogic : IAlbumLogic
    {

        IRepository<Album> repo;

        public AlbumLogic(IRepository<Album> repo)
        {
            this.repo = repo;
        }

        public void Create(Album item)
        {
            if (item.ArtistId < 0)
            {
                ;
                throw new ArgumentException("Artist id not valid");
            }
            if (item.AlbumName == "")
            {
                throw new ArgumentException("Album name too short");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Album Read(int id)
        {
            var album = this.repo.Read(id);
            if (album == null)
            {
                throw new ArgumentException("Album doesnt exist.");
            }
            return this.repo.Read(id);
        }

        public IQueryable<Album> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Album item)
        {
            this.repo.Update(item);
        }

        public IEnumerable<NonCrud.AlbumInfo> GetAlbumsWithMostSongs()
        {

            var albums = this.repo.ReadAll();
            return albums
               
                .Select(a => new NonCrud.AlbumInfo
                {
                    Album = a,
                    SongCount = a.Songs.Count()
                })
                .OrderByDescending(a => a.SongCount)
                .ToList();
        }    
    }
}
