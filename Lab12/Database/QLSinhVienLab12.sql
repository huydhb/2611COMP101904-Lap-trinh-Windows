-- ============================================================================
-- TRƯỜNG ĐẠI HỌC SƯ PHẠM TP. HỒ CHÍ MINH
-- KHOA CÔNG NGHỆ THÔNG TIN
-- BÀI LAB 12: DATA BINDING, STORED PROCEDURE VÀ TRANSACTION
-- SCRIPT CƠ SỞ DỮ LIỆU: QLSinhVienLab12
-- ============================================================================

USE master;
GO

-- 1. TẠO CƠ SỞ DỮ LIỆU
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QLSinhVienLab12')
BEGIN
    CREATE DATABASE QLSinhVienLab12;
END
GO

USE QLSinhVienLab12;
GO

-- 2. TẠO BẢNG LOP
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LOP')
BEGIN
    CREATE TABLE LOP (
        MaLop VARCHAR(10) PRIMARY KEY,
        TenLop NVARCHAR(100) NOT NULL
    );
END
GO

-- 3. TẠO BẢNG SINHVIEN
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SINHVIEN')
BEGIN
    CREATE TABLE SINHVIEN (
        MaSV VARCHAR(10) PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        NgaySinh DATE NULL,
        MaLop VARCHAR(10) NOT NULL,
        CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
    );
END
GO

-- 4. TẠO BẢNG MONHOC
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MONHOC')
BEGIN
    CREATE TABLE MONHOC (
        MaMon VARCHAR(10) PRIMARY KEY,
        TenMon NVARCHAR(100) NOT NULL,
        SoTinChi INT NOT NULL DEFAULT 3
    );
END
GO

-- 5. TẠO BẢNG KETQUA (Khóa chính gồm MaSV, MaMon; Ràng buộc Điểm từ 0 đến 10)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'KETQUA')
BEGIN
    CREATE TABLE KETQUA (
        MaSV VARCHAR(10) NOT NULL,
        MaMon VARCHAR(10) NOT NULL,
        Diem FLOAT NULL,
        GhiChu NVARCHAR(255) NULL,
        CONSTRAINT PK_KETQUA PRIMARY KEY (MaSV, MaMon),
        CONSTRAINT FK_KETQUA_SINHVIEN FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV),
        CONSTRAINT FK_KETQUA_MONHOC FOREIGN KEY (MaMon) REFERENCES MONHOC(MaMon),
        CONSTRAINT CK_KETQUA_Diem CHECK (Diem IS NULL OR (Diem >= 0 AND Diem <= 10))
    );
END
GO

-- ============================================================================
-- 6. CÁC STORED PROCEDURES BẮT BUỘC
-- ============================================================================

-- 6.1 Lấy toàn bộ danh sách lớp
CREATE OR ALTER PROCEDURE sp_Lop_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaLop, TenLop
    FROM LOP
    ORDER BY MaLop;
END
GO

-- 6.2 Lấy toàn bộ danh sách môn học
CREATE OR ALTER PROCEDURE sp_MonHoc_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaMon, TenMon
    FROM MONHOC
    ORDER BY MaMon;
END
GO

-- 6.3 Lấy danh sách sinh viên theo lớp và môn học (kèm điểm nếu đã có)
CREATE OR ALTER PROCEDURE sp_KetQua_GetByLopMon
    @MaLop VARCHAR(10),
    @MaMon VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        sv.MaSV, 
        sv.HoTen, 
        kq.Diem, 
        kq.GhiChu
    FROM SINHVIEN sv
    LEFT JOIN KETQUA kq ON sv.MaSV = kq.MaSV AND kq.MaMon = @MaMon
    WHERE sv.MaLop = @MaLop
    ORDER BY sv.MaSV;
END
GO

-- 6.4 Lưu kết quả: thêm mới nếu chưa có, cập nhật nếu đã tồn tại
CREATE OR ALTER PROCEDURE sp_KetQua_Save
    @MaSV VARCHAR(10),
    @MaMon VARCHAR(10),
    @Diem FLOAT,
    @GhiChu NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM KETQUA WHERE MaSV = @MaSV AND MaMon = @MaMon)
    BEGIN
        UPDATE KETQUA
        SET Diem = @Diem,
            GhiChu = @GhiChu
        WHERE MaSV = @MaSV AND MaMon = @MaMon;
    END
    ELSE
    BEGIN
        INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu)
        VALUES (@MaSV, @MaMon, @Diem, @GhiChu);
    END
END
GO

-- ============================================================================
-- 7. NẠP DỮ LIỆU MẪU (SAMPLE DATA)
-- ============================================================================

-- Dữ liệu LOP
IF NOT EXISTS (SELECT 1 FROM LOP WHERE MaLop = '23CNTT1')
    INSERT INTO LOP (MaLop, TenLop) VALUES ('23CNTT1', N'Công nghệ Thông tin 1');
ELSE
    UPDATE LOP SET TenLop = N'Công nghệ Thông tin 1' WHERE MaLop = '23CNTT1';

IF NOT EXISTS (SELECT 1 FROM LOP WHERE MaLop = '23CNTT2')
    INSERT INTO LOP (MaLop, TenLop) VALUES ('23CNTT2', N'Công nghệ Thông tin 2');
ELSE
    UPDATE LOP SET TenLop = N'Công nghệ Thông tin 2' WHERE MaLop = '23CNTT2';

IF NOT EXISTS (SELECT 1 FROM LOP WHERE MaLop = '23TOAN1')
    INSERT INTO LOP (MaLop, TenLop) VALUES ('23TOAN1', N'Toán - Tin học 1');
ELSE
    UPDATE LOP SET TenLop = N'Toán - Tin học 1' WHERE MaLop = '23TOAN1';
GO

-- Dữ liệu MONHOC
IF NOT EXISTS (SELECT 1 FROM MONHOC WHERE MaMon = 'CSDL')
    INSERT INTO MONHOC (MaMon, TenMon, SoTinChi) VALUES ('CSDL', N'Cơ sở dữ liệu', 3);
ELSE
    UPDATE MONHOC SET TenMon = N'Cơ sở dữ liệu' WHERE MaMon = 'CSDL';

IF NOT EXISTS (SELECT 1 FROM MONHOC WHERE MaMon = 'LTW')
    INSERT INTO MONHOC (MaMon, TenMon, SoTinChi) VALUES ('LTW', N'Lập trình trên Windows', 3);
ELSE
    UPDATE MONHOC SET TenMon = N'Lập trình trên Windows' WHERE MaMon = 'LTW';

IF NOT EXISTS (SELECT 1 FROM MONHOC WHERE MaMon = 'CTDL')
    INSERT INTO MONHOC (MaMon, TenMon, SoTinChi) VALUES ('CTDL', N'Cấu trúc dữ liệu và giải thuật', 4);
ELSE
    UPDATE MONHOC SET TenMon = N'Cấu trúc dữ liệu và giải thuật' WHERE MaMon = 'CTDL';
GO

-- Dữ liệu SINHVIEN
IF NOT EXISTS (SELECT 1 FROM SINHVIEN WHERE MaSV = 'SV01')
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, MaLop) VALUES ('SV01', N'Nguyễn Văn An', '2005-01-15', '23CNTT1');
ELSE
    UPDATE SINHVIEN SET HoTen = N'Nguyễn Văn An' WHERE MaSV = 'SV01';

IF NOT EXISTS (SELECT 1 FROM SINHVIEN WHERE MaSV = 'SV02')
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, MaLop) VALUES ('SV02', N'Trần Thị Mai', '2005-03-22', '23CNTT1');
ELSE
    UPDATE SINHVIEN SET HoTen = N'Trần Thị Mai' WHERE MaSV = 'SV02';

IF NOT EXISTS (SELECT 1 FROM SINHVIEN WHERE MaSV = 'SV03')
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, MaLop) VALUES ('SV03', N'Lê Hoàng Long', '2005-07-10', '23CNTT1');
ELSE
    UPDATE SINHVIEN SET HoTen = N'Lê Hoàng Long' WHERE MaSV = 'SV03';

IF NOT EXISTS (SELECT 1 FROM SINHVIEN WHERE MaSV = 'SV04')
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, MaLop) VALUES ('SV04', N'Phạm Minh Tuấn', '2005-11-05', '23CNTT2');
ELSE
    UPDATE SINHVIEN SET HoTen = N'Phạm Minh Tuấn' WHERE MaSV = 'SV04';

IF NOT EXISTS (SELECT 1 FROM SINHVIEN WHERE MaSV = 'SV05')
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, MaLop) VALUES ('SV05', N'Đỗ Hải Yến', '2005-09-18', '23CNTT2');
ELSE
    UPDATE SINHVIEN SET HoTen = N'Đỗ Hải Yến' WHERE MaSV = 'SV05';

IF NOT EXISTS (SELECT 1 FROM SINHVIEN WHERE MaSV = 'SV06')
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, MaLop) VALUES ('SV06', N'Võ Quốc Huy', '2005-04-30', '23TOAN1');
ELSE
    UPDATE SINHVIEN SET HoTen = N'Võ Quốc Huy' WHERE MaSV = 'SV06';
GO

-- Dữ liệu KETQUA mẫu
IF NOT EXISTS (SELECT 1 FROM KETQUA WHERE MaSV = 'SV01' AND MaMon = 'LTW')
    INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu) VALUES ('SV01', 'LTW', 8.5, N'Đạt kết quả tốt');
ELSE
    UPDATE KETQUA SET Diem = 8.5, GhiChu = N'Đạt kết quả tốt' WHERE MaSV = 'SV01' AND MaMon = 'LTW';

IF NOT EXISTS (SELECT 1 FROM KETQUA WHERE MaSV = 'SV02' AND MaMon = 'LTW')
    INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu) VALUES ('SV02', 'LTW', 9.0, N'Xuất sắc');
ELSE
    UPDATE KETQUA SET Diem = 9.0, GhiChu = N'Xuất sắc' WHERE MaSV = 'SV02' AND MaMon = 'LTW';

-- Dữ liệu môn CSDL
IF NOT EXISTS (SELECT 1 FROM KETQUA WHERE MaSV = 'SV01' AND MaMon = 'CSDL')
    INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu) VALUES ('SV01', 'CSDL', 7.0, N'Đạt');
ELSE
    UPDATE KETQUA SET Diem = 7.0, GhiChu = N'Đạt' WHERE MaSV = 'SV01' AND MaMon = 'CSDL';

IF NOT EXISTS (SELECT 1 FROM KETQUA WHERE MaSV = 'SV04' AND MaMon = 'CSDL')
    INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu) VALUES ('SV04', 'CSDL', 8.0, N'Tốt');
ELSE
    UPDATE KETQUA SET Diem = 8.0, GhiChu = N'Tốt' WHERE MaSV = 'SV04' AND MaMon = 'CSDL';
GO
