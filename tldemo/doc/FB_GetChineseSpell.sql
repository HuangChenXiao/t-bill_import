CREATE FUNCTION FB_GetChineseSpell
(
    @Str   varchar(500)=''
)
RETURNS  varchar(500)
as   
  begin   
    declare @strLen int
    declare @index int
    declare @VChControl char(2)
    DECLARE @VChSpell VARCHAR(30)
    declare @return varchar(500)
    set @return=''
    set @strlen=len(@str)
    set @index=1
    while @index<=@strlen
    begin
        select @VChControl=substring(@str,@index,1)
         IF @VChControl>'��' AND @VChControl<'��' 
            SELECT @VChSpell='A'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='B'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='C'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='D'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='E'
        ELSE IF @VChControl>='��' AND @VChControl<='��'
            SELECT @VChSpell='F'
        ELSE IF @VChControl>'��' AND @VChControl<'��' 
            SELECT @VChSpell='G'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='H'
        ELSE IF @VChControl>='��' AND @VChControl<'��'
            SELECT @VChSpell='J'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='K'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='L'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='M'
        ELSE IF @VChControl>='��' AND @VChControl<'Ŷ' 
            SELECT @VChSpell='N'
        ELSE IF @VChControl>='Ŷ' AND @VChControl<'ž' 
            SELECT @VChSpell='O'
        ELSE IF @VChControl>='ž' AND @VChControl<'��'
            SELECT @VChSpell='P'
        ELSE IF @VChControl>='��' AND @VChControl<'Ȼ' 
            SELECT @VChSpell='Q'
        ELSE IF @VChControl>='Ȼ' AND @VChControl<'��' 
            SELECT @VChSpell='R'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='S'
        ELSE IF @VChControl>='��' AND @VChControl<'��'
            SELECT @VChSpell='T'
        ELSE IF @VChControl>='��' AND @VChControl<'��' 
            SELECT @VChSpell='W'
        ELSE IF @VChControl>='��' AND @VChControl<'ѹ'
            SELECT @VChSpell='X'
        ELSE IF @VChControl>='ѹ' AND @VChControl<'��' 
            SELECT @VChSpell='Y'
        ELSE IF @VChControl>='��' AND @VChControl<='��' 
            SELECT @VChSpell='Z'
        ELSE
            SELECT @VChSpell=@VChControl

        SELECT @return=@return + RTRIM(UPPER(@VChSpell))
        set @index=@index+1
    end
  return(@return)   
  end
  