﻿namespace AdminTemplate.service.Dto.SysUser
{
    public class SysUserLogin
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int? Createby { get; set; }
        public string Openid { get; set; }
        public int? Type { get; set; }
        public string NickName { get; set; }

    }
}
