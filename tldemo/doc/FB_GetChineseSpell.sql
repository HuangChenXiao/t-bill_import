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
         IF @VChControl>'°¡' AND @VChControl<'°Å' 
            SELECT @VChSpell='A'
        ELSE IF @VChControl>='°Å' AND @VChControl<'²Á' 
            SELECT @VChSpell='B'
        ELSE IF @VChControl>='²Á' AND @VChControl<'´î' 
            SELECT @VChSpell='C'
        ELSE IF @VChControl>='´î' AND @VChControl<'¶ð' 
            SELECT @VChSpell='D'
        ELSE IF @VChControl>='¶ð' AND @VChControl<'·¢' 
            SELECT @VChSpell='E'
        ELSE IF @VChControl>='·¢' AND @VChControl<='¸Â'
            SELECT @VChSpell='F'
        ELSE IF @VChControl>'¸Â' AND @VChControl<'¹þ' 
            SELECT @VChSpell='G'
        ELSE IF @VChControl>='¹þ' AND @VChControl<'»÷' 
            SELECT @VChSpell='H'
        ELSE IF @VChControl>='»÷' AND @VChControl<'¿¦'
            SELECT @VChSpell='J'
        ELSE IF @VChControl>='¿¦' AND @VChControl<'À¬' 
            SELECT @VChSpell='K'
        ELSE IF @VChControl>='À¬' AND @VChControl<'Âè' 
            SELECT @VChSpell='L'
        ELSE IF @VChControl>='Âè' AND @VChControl<'ÄÃ' 
            SELECT @VChSpell='M'
        ELSE IF @VChControl>='ÄÃ' AND @VChControl<'Å¶' 
            SELECT @VChSpell='N'
        ELSE IF @VChControl>='Å¶' AND @VChControl<'Å¾' 
            SELECT @VChSpell='O'
        ELSE IF @VChControl>='Å¾' AND @VChControl<'ÆÚ'
            SELECT @VChSpell='P'
        ELSE IF @VChControl>='ÆÚ' AND @VChControl<'È»' 
            SELECT @VChSpell='Q'
        ELSE IF @VChControl>='È»' AND @VChControl<'Èö' 
            SELECT @VChSpell='R'
        ELSE IF @VChControl>='Èö' AND @VChControl<'Ëú' 
            SELECT @VChSpell='S'
        ELSE IF @VChControl>='Ëú' AND @VChControl<'ÍÚ'
            SELECT @VChSpell='T'
        ELSE IF @VChControl>='ÍÚ' AND @VChControl<'Îô' 
            SELECT @VChSpell='W'
        ELSE IF @VChControl>='Îô' AND @VChControl<'Ñ¹'
            SELECT @VChSpell='X'
        ELSE IF @VChControl>='Ñ¹' AND @VChControl<'ÔÑ' 
            SELECT @VChSpell='Y'
        ELSE IF @VChControl>='ÔÑ' AND @VChControl<='×ù' 
            SELECT @VChSpell='Z'
        ELSE
            SELECT @VChSpell=@VChControl

        SELECT @return=@return + RTRIM(UPPER(@VChSpell))
        set @index=@index+1
    end
  return(@return)   
  end
  