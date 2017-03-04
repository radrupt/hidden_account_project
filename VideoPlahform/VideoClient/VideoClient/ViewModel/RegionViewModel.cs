using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VideoCommon.com.pandawork.common.dto;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace VideoClient.ViewModel
{
    public class RegionViewModel : ViewModelBase
    {
        #region  私有成员

        private string name;

        private int level;

        private ObservableCollection<RegionViewModel> childs;

        private RegionDTO regionDTO;

        private RegionViewModel parent;

        private bool isSelected;

        #endregion

        #region 属性

        public NodeInfo NodeInfo { get; private set; }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }

        public string BackgroundImage
        {
            get
            {
                return @"../../Skin/img/tree_area_bg.png";
                // return @"../Skin/img/tree_area_bg/"+level+@".png";
            }
            set
            {
                ;
            }
        }

        public ObservableCollection<RegionViewModel> Childs
        {
            get
            {
                return childs;
            }
            set {
                childs = value;
            }
        }

        public RegionDTO RegionDTO
        {
            get
            {
                return regionDTO;
            }
            set { regionDTO = value;}
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                    OnIsSelectedChanged();
                }
            }
        }

        public RegionViewModel Parent 
        {
            get { return parent; }
            set { parent = value; }
        }
        #endregion

        #region  构造方法
        public RegionViewModel() { }
        public RegionViewModel(string name,RegionViewModel parent = null, NodeInfo nodeinfo=null) 
        {
            childs = new ObservableCollection<RegionViewModel>(); 
            this.parent = parent;
            this.name = name;
            IsSelected = false;
            NodeInfo = nodeinfo;
        }

        public RegionViewModel(string name,int level, RegionViewModel parent = null, NodeInfo nodeinfo = null)
        {
            this.level = level;
            childs = new ObservableCollection<RegionViewModel>();
            this.parent = parent;
            this.name = name;
            IsSelected = false;
            NodeInfo = nodeinfo;
        }

        public RegionViewModel(string name, int level)
        {
            this.name = name;
            this.level = level;
            childs = new ObservableCollection<RegionViewModel>();
        }

        #endregion

        #region 命令及其处理方法
        
        #endregion

        #region  对节点的操作
        public void Remove()
        {
            if (parent != null)
            {
                parent.childs.Remove(this);
            }
        }

        public void Append(RegionViewModel newChild)
        {
            
            childs.Add(newChild);
        }

        public void Rename(string newname)
        {
            Name = newname;
        }

        #endregion

        #region 事件

        public event EventHandler IsSelectedChanged;

        protected virtual void OnIsSelectedChanged()
        {         
            if (IsSelectedChanged != null)
                IsSelectedChanged(this, EventArgs.Empty);
            if (IsSelected)
            {
                NodeInfo.SelectedNode = this;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public  new event PropertyChangedEventHandler  PropertyChanged;

        protected virtual void OnPropertyChanged(string proName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(proName));
        }

        #endregion
    }
}
